namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public abstract class Value
        : ISerializable
    {
        private readonly Lazy<int> hashCode;

        protected Value()
        {
            hashCode = new Lazy<int>(AggregateHashCode);
        }

        protected Value(SerializationInfo info, StreamingContext context)
        {
            hashCode = new Lazy<int>(AggregateHashCode);
        }

        public static bool operator ==(Value first, Value second)
        {
            return EqualOperator(first, second);
        }

        public static bool operator !=(Value first, Value second)
        {
            return NotEqualOperator(first, second);
        }

        public override bool Equals(object other)
        {
            return other is Value value
                ? GetHashCode() == value.GetHashCode()
                : false;
        }

        public override int GetHashCode()
        {
            return hashCode.Value;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        protected int AggregateHashCode()
        {
            return AggregateHashCode(GetAtomicValues());
        }

        protected int AggregateHashCode(IEnumerable<object> values)
        {
            return values.Count() < 2
                ? values.Select(CalculateHashCode).FirstOrDefault()
                : values.Select(CalculateHashCode).Aggregate((first, second) => first ^ second);
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        private static bool EqualOperator(Value left, Value right)
        {
            return left is null ^ right is null
                ? false
                : left is null || left.Equals(right);
        }

        private static bool NotEqualOperator(Value left, Value right)
        {
            return !EqualOperator(left, right);
        }

        private int CalculateHashCode(object value)
        {
            return value is Array array
                ? AggregateHashCode(array.Cast<object>().ToArray())
                : value is null
                    ? 0
                    : value.GetHashCode();
        }
    }
}