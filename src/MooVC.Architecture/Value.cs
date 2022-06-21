namespace MooVC.Architecture;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

[Serializable]
public abstract class Value
    : ISerializable,
      IEquatable<Value>
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

    public static bool operator ==(Value? first, Value? second)
    {
        return EqualOperator(first, second);
    }

    public static bool operator !=(Value? first, Value? second)
    {
        return NotEqualOperator(first, second);
    }

    public override bool Equals(object? other)
    {
        if (other is Value value)
        {
            return Equals(value);
        }

        return false;
    }

    public virtual bool Equals(Value? other)
    {
        if (other is { } && other.GetType() == GetType())
        {
            return other.GetHashCode() == GetHashCode();
        }

        return false;
    }

    public override int GetHashCode()
    {
        return hashCode.Value;
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
    }

    internal static bool EqualOperator(Value? left, Value? right)
    {
        return !(left is null ^ right is null)
            && (left is null || left.Equals(right));
    }

    internal static bool NotEqualOperator(Value? left, Value? right)
    {
        return !EqualOperator(left, right);
    }

    protected int AggregateHashCode()
    {
        IEnumerable<int> aggregation = AggregateHashCode(GetAtomicValues());

        return aggregation.Any()
            ? CalculateHashCode(aggregation)
            : 0;
    }

    protected IEnumerable<int> AggregateHashCode(IEnumerable<object?> values)
    {
        return values.SelectMany(CalculateHashCode);
    }

    protected abstract IEnumerable<object?> GetAtomicValues();

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

    private IEnumerable<int> CalculateHashCode(object? value)
    {
        if (value is Array array)
        {
            return AggregateHashCode(array.Cast<object>());
        }

        return new[] { value?.GetHashCode() ?? 0 };
    }
}