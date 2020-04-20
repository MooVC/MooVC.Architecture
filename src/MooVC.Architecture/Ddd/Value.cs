namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Collections.Generic;
    using MooVC.Linq;

    [Serializable]
    public abstract class Value
        : ISerializable
    {
        private const int MaximumOffset = 32;

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
            IEnumerable<int> aggregation = AggregateHashCode(GetAtomicValues());

            return aggregation.SafeAny()
                ? CalculateHashCode(aggregation)
                : 0;
        }

        protected IEnumerable<int> AggregateHashCode(IEnumerable<object> values)
        {
            return values.SelectMany(CalculateHashCode);
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        private static int CalculateHashCode(IEnumerable<int> aggregation)
        {
            int value = 0;
            int[] hashCodes = aggregation.ToArray();

            for (int index = 0; index < hashCodes.Length; index++)
            {
                int offset = index % MaximumOffset;

                unchecked
                {
                    int hashCode = hashCodes[index];

                    if (offset > 0)
                    {
                        if (hashCode == 0)
                        {
                            value += index * offset;
                        }
                        else
                        {
                            int left = hashCode << (MaximumOffset - offset);
                            int right = hashCode >> offset;

                            value += index * (left | right);
                        }
                    }
                    else
                    {
                        value += hashCode;
                    }
                }
            }

            return value;
        }

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