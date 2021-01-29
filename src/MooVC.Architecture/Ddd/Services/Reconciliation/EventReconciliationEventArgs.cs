namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using MooVC.Collections.Generic;
    using MooVC.Linq;
    using MooVC.Serialization;
    using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public sealed class EventReconciliationEventArgs
        : EventArgs,
          ISerializable
    {
        public EventReconciliationEventArgs(IEnumerable<DomainEvent> events)
        {
            ArgumentIsAcceptable(
                events,
                nameof(events),
                _ => events.SafeAny(),
                EventReconciliationEventArgsEventsRequired);

            Events = events.Snapshot();
        }

        private EventReconciliationEventArgs(SerializationInfo info, StreamingContext context)
        {
            Events = info.TryGetEnumerable<DomainEvent>(nameof(Events));
        }

        public IEnumerable<DomainEvent> Events { get; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _ = info.TryAddEnumerable(nameof(Events), Events);
        }
    }
}