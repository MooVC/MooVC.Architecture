namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class SerializableAggregateRoot
        : AggregateRoot
    {
        public SerializableAggregateRoot()
            : this(Guid.NewGuid())
        {
        }

        public SerializableAggregateRoot(Guid id)
            : base(id)
        {
        }

        private SerializableAggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}