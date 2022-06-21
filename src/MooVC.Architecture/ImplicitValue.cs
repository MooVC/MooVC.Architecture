namespace MooVC.Architecture;

using System.Collections.Generic;
using System.Runtime.Serialization;
using MooVC.Serialization;

public abstract class ImplicitValue<T>
    : Value
    where T : struct
{
    protected ImplicitValue(T value)
    {
        Value = value;
    }

    protected ImplicitValue(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Value = info.TryGetValue<T>(nameof(Value));
    }

    public T Value { get; }

    public static implicit operator T(ImplicitValue<T> value)
    {
        return value.Value;
    }

    public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        _ = info.TryAddValue(nameof(Value), Value);
    }

    protected sealed override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
    }
}