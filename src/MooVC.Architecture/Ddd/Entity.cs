namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Serialization;

    [Serializable]
    public abstract class Entity<T>
        : ISerializable
        where T : notnull
    {
        protected Entity(T id)
        {
            Id = id;
        }

        protected Entity(SerializationInfo info, StreamingContext context)
        {
            Id = info.GetValue<T>(nameof(Id));
        }

        public T Id { get; }

        public static bool operator ==(Entity<T>? first, Entity<T>? second)
        {
            return EqualOperator(first, second);
        }

        public static bool operator !=(Entity<T>? first, Entity<T>? second)
        {
            return NotEqualOperator(first, second);
        }

        public override bool Equals(object? other)
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

        private static bool EqualOperator(Entity<T>? left, Entity<T>? right)
        {
            return left is null ^ right is null
                ? false
                : left is null || left.Equals(right);
        }

        private static bool NotEqualOperator(Entity<T>? left, Entity<T>? right)
        {
            return !EqualOperator(left, right);
        }
    }
}