namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

[Serializable]
public abstract class MultiTypeReference<T1, T2, T3, T4>
    : MultiTypeReference<T1, T2, T3>
    where T1 : AggregateRoot
    where T2 : AggregateRoot
    where T3 : AggregateRoot
    where T4 : AggregateRoot
{
    private readonly Lazy<Reference<T4>> fourth;

    protected MultiTypeReference(
        Reference<T1>? first = default,
        Reference<T2>? second = default,
        Reference<T3>? third = default,
        Reference<T4>? fourth = default)
        : this(new Reference?[] { first, second, third, fourth })
    {
    }

    protected MultiTypeReference(Reference? subject)
        : base(subject)
    {
        fourth = new(ToTyped<T4>);
    }

    protected MultiTypeReference(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        fourth = new(ToTyped<T4>);
    }

    private protected MultiTypeReference(IEnumerable<Reference?> references, params Func<Reference, bool>[] validations)
        : base(references, validations: validations.Prepend(subject => subject.Is<T4>(out _)))
    {
        fourth = new(ToTyped<T4>);
    }

    protected bool IsFourth => !Fourth.IsEmpty;

    protected Reference<T4> Fourth => fourth.Value;

    public static implicit operator Reference<T4>(MultiTypeReference<T1, T2, T3, T4> reference)
    {
        return reference.Fourth;
    }

    public static bool operator ==(MultiTypeReference<T1, T2, T3, T4> reference, Reference<T4>? other)
    {
        return reference.Equals(other);
    }

    public static bool operator !=(MultiTypeReference<T1, T2, T3, T4> reference, Reference<T4>? other)
    {
        return reference.Equals(other);
    }

    public bool Equals(Reference<T4>? other)
    {
        return Fourth == other;
    }
}