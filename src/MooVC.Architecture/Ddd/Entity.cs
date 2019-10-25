namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public abstract class Entity<T>
        : ISerializable
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

        public static bool operator ==(Entity<T> first, Entity<T> second)
        {
            return EqualOperator(first, second);
        }

        public static bool operator !=(Entity<T> first, Entity<T> second)
        {
            return NotEqualOperator(first, second);
        }

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

        private static bool EqualOperator(Entity<T> left, Entity<T> right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        private static bool NotEqualOperator(Entity<T> left, Entity<T> right)
        {
            return !EqualOperator(left, right);
        }
    }
}