namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;
    using MooVC.Collections.Generic;
    using MooVC.Serialization;
    using static MooVC.Architecture.Ddd.Services.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract class AtomicUnit<T>
        : ISerializable
    {
        private readonly Lazy<VersionedReference> aggregate;

        protected AtomicUnit(T id, params DomainEvent[] events)
        {
            ArgumentIsAcceptable(
                events,
                nameof(events),
                value => value.Any(),
                AtomicUnitEventsRequired);

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
            Id = id;
        }

        protected AtomicUnit(SerializationInfo info, StreamingContext context)
        {
            aggregate = new Lazy<VersionedReference>(IdentifyAggregate);
            Events = info.GetEnumerable<DomainEvent>(nameof(Events));
            Id = info.GetValue<T>(nameof(Id));
        }

        public VersionedReference Aggregate => aggregate.Value;

        public IEnumerable<DomainEvent> Events { get; }

        public T Id { get; }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddEnumerable(nameof(Events), Events);
            info.AddValue(nameof(Id), Id);
        }

        private static bool HasSame<TValue>(DomainEvent[] events, Func<DomainEvent, TValue> selector)
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