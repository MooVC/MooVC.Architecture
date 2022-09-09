namespace MooVC.Architecture.Ddd.MultiTypeReferenceTests;

using System;
using System.Runtime.Serialization;

[Serializable]
internal sealed class TestableMultiTypeReference<T1, T2, T3, T4, T5>
    : MultiTypeReference<T1, T2, T3, T4, T5>
    where T1 : AggregateRoot
    where T2 : AggregateRoot
    where T3 : AggregateRoot
    where T4 : AggregateRoot
    where T5 : AggregateRoot
{
    public TestableMultiTypeReference(
        Reference<T1>? first = default,
        Reference<T2>? second = default,
        Reference<T3>? third = default,
        Reference<T4>? fourth = default,
        Reference<T5>? fifth = default)
        : base(first: first, second: second, third: third, fourth: fourth, fifth: fifth)
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

    public new bool IsThird => base.IsThird;

    public new bool IsFourth => base.IsFourth;

    public new bool IsFifth => base.IsFifth;
}