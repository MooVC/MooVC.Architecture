namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;

    [Serializable]
    public sealed class AtomicUnit
        : AtomicUnit<Guid>
    {
        public AtomicUnit(params DomainEvent[] events)
            : base(Guid.NewGuid(), events)
        {
        }

        private AtomicUnit(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}