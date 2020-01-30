namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Collections.Generic;

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
            int value = 0;
            bool shouldInvert = false;

            AggregateHashCode(GetAtomicValues()).ForEach(code =>
            {
                if (shouldInvert)
                {
                    byte[] bytes = BitConverter.GetBytes(code);

                    bytes = bytes.Reverse().ToArray();

                    code = BitConverter.ToInt32(bytes, 0);
                }

                unchecked
                {
                    value += code;
                }

                shouldInvert = !shouldInvert;
            });

            return value;
        }

        protected IEnumerable<int> AggregateHashCode(IEnumerable<object> values)
        {
            return values.SelectMany(CalculateHashCode);
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

        private IEnumerable<int> CalculateHashCode(object value)
        {
            if (value is Array array)
            {
                return AggregateHashCode(array.Cast<object>());
            }

            return new[] { value?.GetHashCode() ?? 0 };
        }
    }
}