namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public abstract class Reference<T1, T2, T3, T4, T5>
    : Reference<T1, T2, T3, T4>
    where T1 : AggregateRoot
    where T2 : AggregateRoot
    where T3 : AggregateRoot
    where T4 : AggregateRoot
    where T5 : AggregateRoot
{
    private readonly Lazy<Reference<T5>> fifth;

    protected Reference(
        Reference<T1>? first = default,
        Reference<T2>? second = default,
        Reference<T3>? third = default,
        Reference<T4>? fourth = default,
        Reference<T5>? fifth = default,
        bool unversioned = true)
        : this(new Reference?[] { first, second, third, fourth, fifth }, unversioned)
    {
    }

    protected Reference(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        fifth = new(ToTyped<T5>);
    }

    protected Reference()
    {
        fifth = new(ToTyped<T5>);
    }

    private protected Reference(IEnumerable<Reference?> references, bool unversioned, params Func<Reference, bool>[] validations)
        : base(references, unversioned, validations: validations.Prepend(subject => subject.Is<T5>(out _)))
    {
        fifth = new(ToTyped<T5>);
    }

    protected bool IsFifth => !Fifth.IsEmpty;

    protected Reference<T5> Fifth => fifth.Value;

    public static implicit operator Reference<T5>(Reference<T1, T2, T3, T4, T5> reference)
    {
        return reference.Fifth;
    }

    public static bool operator ==(Reference<T1, T2, T3, T4, T5> reference, Reference<T5>? other)
    {
        return reference.Equals(other);
    }

    public static bool operator !=(Reference<T1, T2, T3, T4, T5> reference, Reference<T5>? other)
    {
        return !reference.Equals(other);
    }

    public bool Equals(Reference<T5>? other)
    {
        return Fifth == other;
    }
}