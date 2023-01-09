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

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
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

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public sealed override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        _ = info.TryAddValue(nameof(Value), Value);
    }

    protected sealed override IEnumerable<object?> GetAtomicValues()
    {
        yield return Value;
    }
}