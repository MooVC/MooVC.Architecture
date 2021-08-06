namespace MooVC.Architecture.Ddd.DomainExceptionTests
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture;

    [Serializable]
    public sealed class SerializableDomainException<TAggregate>
        : DomainException<TAggregate>
        where TAggregate : AggregateRoot
    {
        public SerializableDomainException(
            Message context,
            Reference<TAggregate> aggregate,
            string message)
            : base(context, aggregate, message)
        {
        }

        public SerializableDomainException(
            Message context,
            TAggregate aggregate,
            string message)
            : base(context, aggregate, message)
        {
        }

        private SerializableDomainException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}