namespace MooVC.Architecture.ValueTests;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MooVC.Collections.Generic;
using MooVC.Serialization;

[Serializable]
internal sealed class SerializableValue
    : Value
{
    public SerializableValue(int first = 0, string? second = default, Value? third = default, IEnumerable<string>? fourth = default)
    {
        First = first;
        Second = second;
        Third = third;
        Fourth = fourth.Snapshot();
    }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public SerializableValue(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        First = info.TryGetValue<int>(nameof(First));
        Second = info.TryGetValue<string>(nameof(Second));
        Third = info.TryGetValue<Value>(nameof(Third));
        Fourth = info.TryGetEnumerable<string>(nameof(Fourth));
    }

    public int First { get; }

    public string? Second { get; }

    public Value? Third { get; }

    public IEnumerable<string> Fourth { get; }

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddValue(nameof(First), First);
        _ = info.TryAddValue(nameof(Second), Second);
        _ = info.TryAddValue(nameof(Third), Third);
        _ = info.TryAddEnumerable(nameof(Fourth), Fourth);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return First;
        yield return Second!;
        yield return Third!;
        yield return Fourth;
    }
}