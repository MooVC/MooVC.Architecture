namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public abstract class AggregateRoot 
        : Entity<Guid>
    {
        public const ulong DefaultVersion = 1;

        protected AggregateRoot(Guid id, ulong version = DefaultVersion)
            : base(id)
        {
            Version = version;
        }

        public ulong Version { get; private protected set; }
        
        protected AggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}