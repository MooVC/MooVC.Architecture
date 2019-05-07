namespace MooVC.Architecture.Ddd
{
    using System;

    public abstract class AggregateRoot 
        : Entity<Guid>
    {
        protected AggregateRoot(Guid id)
            : base(id)
        {
        }
    }
}