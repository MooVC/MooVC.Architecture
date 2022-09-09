namespace MooVC.Architecture.Ddd.MultiTypeReferenceTests;

using System;
using System.Runtime.Serialization;

[Serializable]
internal sealed class TestableMultiTypeReference<T1, T2, T3>
    : MultiTypeReference<T1, T2, T3>
    where T1 : AggregateRoot
    where T2 : AggregateRoot
    where T3 : AggregateRoot
{
    public TestableMultiTypeReference(Reference<T1>? first = default, Reference<T2>? second = default, Reference<T3>? third = default)
        : base(first: first, second: second, third: third)
    {
    }

    private TestableMultiTypeReference(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}