namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class AggregateRoot 
        : Entity<Guid>
    {
        protected AggregateRoot(Guid id)
            : base(id)
        {
        }
        
        protected AggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}