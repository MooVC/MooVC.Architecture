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
    using static Resources;

    [Serializable]
    public sealed class AtomicUnit
        : ISerializable
    {
        public AtomicUnit(params DomainEvent[] events)
        {
            if (!events.SafeAny())
            {
                throw new ArgumentNullException(AtomicUnitEventsRequired);
            }

            if (events.Select(@event => @event.Aggregate).Distinct().Count() > 1)
            {
                throw new ArgumentNullException(AtomicUnitDistinctAggregateVersionRequired);
            }

            Events = events.Snapshot();
        }

        private AtomicUnit(SerializationInfo info, StreamingContext context)
        {
            Events = (DomainEvent[])info.GetValue(nameof(Events), typeof(DomainEvent[]));
            Id = (Guid)info.GetValue(nameof(Id), typeof(Guid));
        }

        public IEnumerable<DomainEvent> Events { get; }

        public Guid Id { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Events), Events.ToArray());
            info.AddValue(nameof(Id), Id);
        }
    }
}