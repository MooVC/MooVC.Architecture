namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Architecture.Ddd;
    using MooVC.Collections.Generic;
    using MooVC.Linq;
    using MooVC.Serialization;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public sealed class AtomicUnit
        : ISerializable
    {
        private readonly Lazy<VersionedReference> aggregate;

        public AtomicUnit(params DomainEvent[] events)
        {
            ArgumentIsAcceptable(events, nameof(events), value => value.SafeAny(), AtomicUnitEventsRequired);

            ArgumentIsAcceptable(
                events,
                nameof(events),
                HasSameAggregate,
                AtomicUnitDistinctAggregateVersionRequired);

            ArgumentIsAcceptable(
                events,
                nameof(events),
                HasSameContext,
                AtomicUnitDistinctContextRequired);

            aggregate = new Lazy<VersionedReference>(IdentifyAggregate);
            Events = events.Snapshot();
            Id = Guid.NewGuid();
        }

        private AtomicUnit(SerializationInfo info, StreamingContext context)
        {
            aggregate = new Lazy<VersionedReference>(IdentifyAggregate);
            Events = info.GetEnumerable<DomainEvent>(nameof(Events));
            Id = info.GetValue<Guid>(nameof(Id));
        }

        public VersionedReference Aggregate => aggregate.Value;

        public IEnumerable<DomainEvent> Events { get; }

        public Guid Id { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddEnumerable(nameof(Events), Events);
            info.AddValue(nameof(Id), Id);
        }

        private static bool HasSame<T>(DomainEvent[] events, Func<DomainEvent, T> selector)
        {
            return events.Select(selector).Distinct().Count() == 1;
        }

        private static bool HasSameAggregate(DomainEvent[] events)
        {
            return HasSame(events, @event => @event.Aggregate);
        }

        private static bool HasSameCausationId(DomainEvent[] events)
        {
            return HasSame(events, @event => @event.CausationId);
        }

        private static bool HasSameContext(DomainEvent[] events)
        {
            return HasSameCausationId(events) && HasSameCorrelationId(events);
        }

        private static bool HasSameCorrelationId(DomainEvent[] events)
        {
            return HasSame(events, @event => @event.CorrelationId);
        }

        private VersionedReference IdentifyAggregate()
        {
            return Events.First().Aggregate;
        }
    }
}