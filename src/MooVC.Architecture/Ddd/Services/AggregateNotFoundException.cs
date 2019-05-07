namespace MooVC.Architecture.Ddd.Services
{
    using System;

    [Serializable]
    public sealed class AggregateNotFoundException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        internal AggregateNotFoundException(Message context, Guid aggregateId)
            : base(string.Format(
                Resources.AggregateNotFoundExceptionMessage,
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