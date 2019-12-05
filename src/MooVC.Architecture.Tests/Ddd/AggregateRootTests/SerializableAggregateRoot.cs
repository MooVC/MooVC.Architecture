namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class SerializableAggregateRoot
        : AggregateRoot
    {
        public SerializableAggregateRoot()
            : this(Guid.NewGuid(), version: DefaultVersion)
        {
        }

        public SerializableAggregateRoot(Guid id, ulong version = DefaultVersion)
            : base(id, version)
        {
        }

        private SerializableAggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}