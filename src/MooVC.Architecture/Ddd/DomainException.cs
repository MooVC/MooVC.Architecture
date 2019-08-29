namespace MooVC.Architecture.Ddd
{
    using System;

    [Serializable]
    public abstract class DomainException
        : InvalidOperationException
    {
        protected DomainException(Message context, AggregateRoot aggregate, string message)
            : this(context, aggregate.ToVersionedReference(), message)
        {
        }

        protected DomainException(Message context, VersionedReference aggregate, string message)
            : base(message)
        {
            Aggregate = aggregate;
            Context = context;
        }

        public VersionedReference Aggregate { get; }

        public Message Context { get; }

        public DateTime TimeStamp { get; } = DateTime.UtcNow;
    }
}
