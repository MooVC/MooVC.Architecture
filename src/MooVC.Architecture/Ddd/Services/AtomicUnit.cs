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
        public AtomicUnit(params DomainEvent[] events)
        {
            ArgumentIsAcceptable(events, nameof(events), value => value.SafeAny(), AtomicUnitEventsRequired);

            ArgumentIsAcceptable(
                events,
                nameof(events),
                value => value.Select(@event => @event.Aggregate).Distinct().Count() == 1,
                AtomicUnitDistinctAggregateVersionRequired);

            Events = events.Snapshot();
            Id = Guid.NewGuid();
        }

        private AtomicUnit(SerializationInfo info, StreamingContext context)
        {
            Events = info.GetEnumerable<DomainEvent>(nameof(Events));
            Id = (Guid)info.GetValue(nameof(Id), typeof(Guid));
        }

        public IEnumerable<DomainEvent> Events { get; }

        public Guid Id { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddEnumerable(nameof(Events), Events);
            info.AddValue(nameof(Id), Id);
        }
    }
}