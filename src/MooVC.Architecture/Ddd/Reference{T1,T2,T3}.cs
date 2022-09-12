namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public abstract class Reference<T1, T2, T3>
    : Reference<T1, T2>
    where T1 : AggregateRoot
    where T2 : AggregateRoot
    where T3 : AggregateRoot
{
    private readonly Lazy<Reference<T3>> third;

    protected Reference(
        Reference<T1>? first = default,
        Reference<T2>? second = default,
        Reference<T3>? third = default,
        bool unversioned = true)
        : this(new Reference?[] { first, second, third }, unversioned)
    {
    }

    protected Reference()
    {
        third = new(ToTyped<T3>);
    }

    protected Reference(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        third = new(ToTyped<T3>);
    }

    private protected Reference(IEnumerable<Reference?> references, bool unversioned, params Func<Reference, bool>[] validations)
        : base(references, unversioned, validations: validations.Prepend(subject => subject.Is<T3>(out _)))
    {
        third = new(ToTyped<T3>);
    }

    protected bool IsThird => !Third.IsEmpty;

    protected Reference<T3> Third => third.Value;

    public static implicit operator Reference<T3>(Reference<T1, T2, T3> reference)
    {
        return reference.Third;
    }

    public static bool operator ==(Reference<T1, T2, T3> reference, Reference<T3>? other)
    {
        return reference.Equals(other);
    }

    public static bool operator !=(Reference<T1, T2, T3> reference, Reference<T3>? other)
    {
        return !reference.Equals(other);
    }

    public bool Equals(Reference<T3>? other)
    {
        return Third == other;
    }
}