namespace MooVC.Architecture.Ddd
{
    using System;

    [Serializable]
    public abstract class DomainException<TAggregate>
        : InvalidOperationException
        where TAggregate : AggregateRoot
    {
        protected DomainException(Message context, Reference<TAggregate> aggregate, string message)
            : base(message)
        {
            Aggregate = aggregate;
            Context = context;
        }

        public Reference<TAggregate> Aggregate { get; }

        public Message Context { get; }

        public DateTime TimeStamp { get; } = DateTime.UtcNow;
    }
}
