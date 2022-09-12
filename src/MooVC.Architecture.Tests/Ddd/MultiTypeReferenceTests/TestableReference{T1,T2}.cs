namespace MooVC.Architecture.Ddd.ReferenceTests;

using System;
using System.Runtime.Serialization;

[Serializable]
internal sealed class TestableReference<T1, T2>
    : Reference<T1, T2>
    where T1 : AggregateRoot
    where T2 : AggregateRoot
{
    public TestableReference(
        Reference<T1>? first = default,
        Reference<T2>? second = default,
        bool unversioned = true)
        : base(first: first, second: second, unversioned: unversioned)
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
}