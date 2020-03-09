namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Serialization;

    [Serializable]
    public abstract class Reference
        : Value
    {
        protected Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Id = info.TryGetValue<Guid>(nameof(Id));
        }

        private protected Reference(Guid id)
        {
            Id = id;
        }

        private protected Reference(AggregateRoot aggregate)
        {
            Id = aggregate.Id;
        }

        public Guid Id { get; }

        public bool IsEmpty => Id == Guid.Empty;

        public abstract Type Type { get; }

        public static bool operator ==(Reference first, Reference second)
        {
            return EqualOperator(first, second);
        }

        public static bool operator !=(Reference first, Reference second)
        {
            return NotEqualOperator(first, second);
        }

        public override bool Equals(object other)
        {
            return EqualOperator(this, other as Reference);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddValue(nameof(Id), Id);
        }

        public virtual bool IsMatch(AggregateRoot aggregate)
        {
            return Type == aggregate?.GetType()
                ? Id == aggregate.Id
                : false;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Type;
        }

        private static bool EqualOperator(Reference left, Reference right)
        {
            return left is null ^ right is null
                ? false
                : left is null
                    ? true
                    : left.Id == right.Id && left.Type == right.Type;
        }

        private static bool NotEqualOperator(Reference left, Reference right)
        {
            return !EqualOperator(left, right);
        }
    }
}