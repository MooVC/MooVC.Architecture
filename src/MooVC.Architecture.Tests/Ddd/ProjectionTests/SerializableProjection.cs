namespace MooVC.Architecture.Ddd.ProjectionTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class SerializableProjection<TAggregate>
        : Projection<TAggregate>
        where TAggregate : AggregateRoot
    {
        public SerializableProjection(TAggregate aggregate)
            : base(aggregate)
        {
        }

        public SerializableProjection(Reference<TAggregate> aggregate)
            : base(aggregate)
        {
        }

        private SerializableProjection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}