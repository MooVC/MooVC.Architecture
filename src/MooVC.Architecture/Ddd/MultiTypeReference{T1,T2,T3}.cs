namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public abstract class MultiTypeReference<T1, T2, T3>
    : MultiTypeReference<T1, T2>
    where T1 : AggregateRoot
    where T2 : AggregateRoot
    where T3 : AggregateRoot
{
    private readonly Lazy<Reference<T3>> third;

    protected MultiTypeReference(Reference<T1>? first = default, Reference<T2>? second = default, Reference<T3>? third = default)
        : this(new Reference?[] { first, second, third })
    {
    }

    protected MultiTypeReference()
    {
        third = new(ToTyped<T3>);
    }

    protected MultiTypeReference(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        third = new(ToTyped<T3>);
    }

    private protected MultiTypeReference(IEnumerable<Reference?> references, params Func<Reference, bool>[] validations)
        : base(references, validations: validations.Prepend(subject => subject.Is<T3>(out _)))
    {
        third = new(ToTyped<T3>);
    }

    protected bool IsThird => !Third.IsEmpty;

    protected Reference<T3> Third => third.Value;

    public static implicit operator Reference(MultiTypeReference<T1, T2, T3> reference)
    {
        return reference.Subject;
    }

    public static implicit operator Reference<T3>(MultiTypeReference<T1, T2, T3> reference)
    {
        return reference.Third;
    }

    public static bool operator ==(MultiTypeReference<T1, T2, T3> reference, Reference<T3>? other)
    {
        return reference.Equals(other);
    }

    public static bool operator !=(MultiTypeReference<T1, T2, T3> reference, Reference<T3>? other)
    {
        return !reference.Equals(other);
    }

    public bool Equals(Reference<T3>? other)
    {
        return Third == other;
    }
}