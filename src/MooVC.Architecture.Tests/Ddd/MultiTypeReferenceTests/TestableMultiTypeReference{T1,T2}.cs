﻿namespace MooVC.Architecture.Ddd.MultiTypeReferenceTests;

using System;
using System.Runtime.Serialization;

[Serializable]
internal sealed class TestableMultiTypeReference<T1, T2>
    : MultiTypeReference<T1, T2>
    where T1 : AggregateRoot
    where T2 : AggregateRoot
{
    public TestableMultiTypeReference(Reference<T1>? first = default, Reference<T2>? second = default)
        : base(first: first, second: second)
    {
    }

    private TestableMultiTypeReference(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}