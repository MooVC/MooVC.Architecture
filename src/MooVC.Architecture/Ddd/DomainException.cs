namespace MooVC.Architecture.Ddd
{
    using System;

    [Serializable]
    public abstract class DomainException
        : InvalidOperationException
    {
        protected DomainException(Message context, IReference aggregate, string message)
            : base(message)
        {
            Aggregate = aggregate;
            Context = context;
        }

        public IReference Aggregate { get; }

        public Message Context { get; }

        public DateTime TimeStamp { get; } = DateTime.UtcNow;
    }
}
