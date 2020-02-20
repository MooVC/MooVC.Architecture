namespace MooVC.Architecture.Ddd
{
    using System;
    using static System.String;
    using static Resources;

    [Serializable]
    public sealed class AggregateDoesNotExistException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateDoesNotExistException(Message context)
            : base(Format(AggregateDoesNotExistExceptionMessage, typeof(TAggregate).Name))
        {
            Context = context;
        }

        public Message Context { get; }
    }
}