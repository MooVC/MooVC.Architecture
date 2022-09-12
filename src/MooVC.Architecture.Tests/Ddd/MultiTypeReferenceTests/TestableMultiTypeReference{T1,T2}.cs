namespace MooVC.Architecture.Ddd.MultiTypeReferenceTests;

using System;
using System.Runtime.Serialization;

[Serializable]
internal sealed class TestableMultiTypeReference<T1, T2>
    : MultiTypeReference<T1, T2>
    where T1 : AggregateRoot
    where T2 : AggregateRoot
{
    public TestableMultiTypeReference(
        Reference<T1>? first = default,
        Reference<T2>? second = default,
        bool unversioned = true)
        : base(first: first, second: second, unversioned: unversioned)
    {
    }

    public TestableMultiTypeReference()
    {
    }

    private TestableMultiTypeReference(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public new bool IsFirst => base.IsFirst;

    public new bool IsSecond => base.IsSecond;
}