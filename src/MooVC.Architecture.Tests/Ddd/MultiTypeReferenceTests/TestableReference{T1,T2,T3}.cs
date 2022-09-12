namespace MooVC.Architecture.Ddd.ReferenceTests;

using System;
using System.Runtime.Serialization;

[Serializable]
internal sealed class TestableReference<T1, T2, T3>
    : Reference<T1, T2, T3>
    where T1 : AggregateRoot
    where T2 : AggregateRoot
    where T3 : AggregateRoot
{
    public TestableReference(
        Reference<T1>? first = default,
        Reference<T2>? second = default,
        Reference<T3>? third = default,
        bool unversioned = true)
        : base(first: first, second: second, third: third, unversioned: unversioned)
    {
    }

    public TestableReference()
    {
    }

    private TestableReference(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public new bool IsFirst => base.IsFirst;

    public new bool IsSecond => base.IsSecond;

    public new bool IsThird => base.IsThird;
}