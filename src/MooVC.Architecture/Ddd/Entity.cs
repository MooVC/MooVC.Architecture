namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public abstract class Entity<T>
    {
        protected Entity(T id)
        {
            Id = id;
        }

        protected Entity(SerializationInfo info, StreamingContext context)
        {
            Id = (T)info.GetValue(nameof(Id), typeof(T));
        }

        public T Id { get; }

        public override bool Equals(object other)
        {
            return other is Entity<T> entity
                && Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
        }
    }
}