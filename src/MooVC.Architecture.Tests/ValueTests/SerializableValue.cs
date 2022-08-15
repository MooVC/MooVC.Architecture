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