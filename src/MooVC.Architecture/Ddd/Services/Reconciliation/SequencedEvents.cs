namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;

    [Serializable]
    public sealed class SequencedEvents
        : AtomicUnit<ulong>,
          ISequencedEvents
    {
        public SequencedEvents(ulong sequence, params DomainEvent[] events)
            : base(sequence, events)
        {
        }

        private SequencedEvents(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ulong Sequence => Id;
    }
}