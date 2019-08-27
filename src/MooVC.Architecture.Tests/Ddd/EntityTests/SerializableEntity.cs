namespace MooVC.Architecture.Ddd.EntityTests
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal sealed class SerializableEntity
        : Entity<Guid>
    {
        public SerializableEntity(Guid id) 
            : base(id)
        {
        }

        private SerializableEntity(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}