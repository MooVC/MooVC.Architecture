namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using static Resources;

    [Serializable]
    public sealed class AggregateNotFoundException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateNotFoundException(Message context, Guid aggregateId)
            : base(string.Format(
                AggregateNotFoundExceptionMessage,
                aggregateId,
                typeof(TAggregate).Name))
        {
            AggregateId = aggregateId;
            Context = context;
        }

        public Guid AggregateId { get; }

        public Message Context { get; }
    }
}