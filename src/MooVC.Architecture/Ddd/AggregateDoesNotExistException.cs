namespace MooVC.Architecture.Ddd
{
    using System;

    [Serializable]
    public sealed class AggregateDoesNotExistException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        internal AggregateDoesNotExistException(Message context)
            : base(string.Format(
                Resources.AggregateDoesNotExistExceptionMessage,
                typeof(TAggregate).Name))
        {
            Context = context;
        }

        public Message Context { get; }
    }
}