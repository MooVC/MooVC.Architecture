namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MooVC.Architecture;
using MooVC.Architecture.Ddd.Serialization;
using static MooVC.Architecture.Ddd.Resources;

[Serializable]
public abstract class MultiTypeReference<T1, T2>
    : Value
    where T1 : AggregateRoot
    where T2 : AggregateRoot
{
    private readonly Lazy<Reference<T1>> first;
    private readonly Lazy<Reference<T2>> second;
    private readonly Reference subject;

    protected MultiTypeReference(Reference<T1>? first = default, Reference<T2>? second = default)
        : this(new Reference?[] { first, second })
    {
    }

    protected MultiTypeReference(Reference? subject)
    {
        this.subject = subject ?? Reference<T1>.Empty;

        first = new(ToTyped<T1>);
        second = new(ToTyped<T2>);
    }

    protected MultiTypeReference(SerializationInfo info, StreamingContext context)
        : this(default(Reference))
    {
        subject = info.TryGetInternalReference(nameof(subject));
    }

    private protected MultiTypeReference(IEnumerable<Reference?> references, params Func<Reference, bool>[] validations)
        : this(Validate(
            references,
            validations: validations
                .Prepend(subject => subject.Is<T2>(out _))
                .Prepend(subject => subject.Is<T1>(out _))))
    {
    }

    protected bool IsFirst => !First.IsEmpty;

    protected bool IsSecond => !Second.IsEmpty;

    protected Reference<T1> First => first.Value;

    protected Reference<T2> Second => second.Value;

    public static implicit operator Reference<T1>(MultiTypeReference<T1, T2> reference)
    {
        return reference.First;
    }

    public static implicit operator Reference<T2>(MultiTypeReference<T1, T2> reference)
    {
        return reference.Second;
    }

    public static bool operator ==(MultiTypeReference<T1, T2> reference, Reference<T1>? other)
    {
        return reference.Equals(other);
    }

    public static bool operator ==(MultiTypeReference<T1, T2> reference, Reference<T2>? other)
    {
        return reference.Equals(other);
    }

    public static bool operator !=(MultiTypeReference<T1, T2> reference, Reference<T1>? other)
    {
        return reference.Equals(other);
    }

    public static bool operator !=(MultiTypeReference<T1, T2> reference, Reference<T2>? other)
    {
        return reference.Equals(other);
    }

    public override bool Equals(object? other)
    {
        if (other is Reference refernece)
        {
            return refernece == subject;
        }

        return base.Equals(other);
    }

    public bool Equals(Reference<T1>? other)
    {
        return First == other;
    }

    public bool Equals(Reference<T2>? other)
    {
        return Second == other;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddInternalReference(nameof(subject), subject);
    }

    public override int GetHashCode()
    {
        return subject.GetHashCode();
    }

    protected static Reference<T> ToTyped<T>(int index, params Reference[] references)
        where T : AggregateRoot
    {
        return references.ElementAtOrDefault(index).ToTyped<T>();
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return subject;
    }

    protected Reference<T> ToTyped<T>()
        where T : AggregateRoot
    {
        return subject.ToTyped<T>();
    }

    private static Reference Select(IEnumerable<Reference?> references)
    {
        try
        {
            return references.Single(reference => reference is { } && !reference.IsEmpty)!;
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException(MultiTypeReferenceReferenceRequired);
        }
    }

    private static Reference Validate(IEnumerable<Reference?> references, IEnumerable<Func<Reference, bool>> validations)
    {
        Reference subject = Select(references);

        if (validations.Any(validation => validation(subject)))
        {
            return subject;
        }

        throw new ArgumentException(MultiTypeReferenceReferenceRequired);
    }
}