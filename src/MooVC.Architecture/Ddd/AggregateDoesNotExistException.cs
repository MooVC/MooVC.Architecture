namespace MooVC.Architecture.Ddd
{
    using System;
    using static Resources;

    [Serializable]
    public sealed class AggregateDoesNotExistException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateDoesNotExistException(Message context)
            : base(string.Format(
                AggregateDoesNotExistExceptionMessage,
                typeof(TAggregate).Name))
        {
            Context = context;
        }

        public Message Context { get; }
    }
}