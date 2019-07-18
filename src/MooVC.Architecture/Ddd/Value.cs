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
        protected readonly Lazy<int> HashCode;

        protected Value()
        {
            HashCode = new Lazy<int>(AggregateHashCode);
        }

        protected Value(SerializationInfo info, StreamingContext context)
        {
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
            var value = other as Value;

            if (value == null)
            {
                return false;
            }

            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = value.GetAtomicValues().GetEnumerator();

            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^
                    ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (!(thisValues.Current == null || 
                      thisValues.Current.Equals(otherValues.Current)))
                {
                    return false;
                }
            }

            return !(thisValues.MoveNext() || otherValues.MoveNext());
        }

        public override int GetHashCode()
        {
            return HashCode.Value;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        protected static bool EqualOperator(Value left, Value right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(Value left, Value right)
        {
            return !EqualOperator(left, right);
        }

        protected int AggregateHashCode()
        {
            return GetAtomicValues()
                .Select(value => value?.GetHashCode() ?? 0)
                .Aggregate((first, second) => first ^ second);
        }

        protected abstract IEnumerable<object> GetAtomicValues();
    }
}