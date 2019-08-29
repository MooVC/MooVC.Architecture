namespace MooVC.Architecture.Ddd
{
    using System;

    [Serializable]
    public abstract class DomainException
        : InvalidOperationException
    {
        protected DomainException(Message context, AggregateRoot aggregate, string message)
            : this(context, aggregate.ToReference(), message)
        {
        }

        protected DomainException(Message context, Reference aggregate, string message)
            : base(message)
        {
            Aggregate = aggregate;
            Context = context;
        }

        public Reference Aggregate { get; }

        public Message Context { get; }

        public DateTime TimeStamp { get; } = DateTime.UtcNow;
    }
}
