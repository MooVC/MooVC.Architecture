namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;
using MooVC.Architecture.Ddd.Serialization;
using static MooVC.Architecture.Ddd.Ensure;
using static MooVC.Architecture.Ddd.Resources;

[Serializable]
public abstract class Projection<TAggregate>
    : ISerializable,
      IEquatable<Reference>
    where TAggregate : AggregateRoot
{
    protected Projection(TAggregate aggregate)
        : this(CreateReference(aggregate))
    {
    }

    protected Projection(Reference<TAggregate> aggregate)
    {
        Aggregate = IsNotEmpty(aggregate, message: ProjectionAggregateRequired);
    }

    protected Projection(SerializationInfo info, StreamingContext context)
    {
        Aggregate = info.TryGetReference<TAggregate>(nameof(Aggregate));
    }

    public Reference<TAggregate> Aggregate { get; }

    public static implicit operator Reference<TAggregate>(Projection<TAggregate>? projection)
    {
        return projection?.Aggregate ?? Reference<TAggregate>.Empty;
    }

    public static bool operator ==(Projection<TAggregate>? first, Reference? second)
    {
        return EqualOperator(first, second);
    }

    public static bool operator !=(Projection<TAggregate>? first, Reference? second)
    {
        return NotEqualOperator(first, second);
    }

    public override bool Equals(object? other)
    {
        if (other is Projection<TAggregate> projection)
        {
            return Aggregate == projection.Aggregate;
        }

        if (other is Reference reference)
        {
            return Equals(reference);
        }

        return false;
    }

    public bool Equals(Reference? other)
    {
        return Aggregate == other;
    }

    public override int GetHashCode()
    {
        return Aggregate.GetHashCode();
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        _ = info.TryAddReference(nameof(Aggregate), Aggregate);
    }

    public override string ToString()
    {
        return Aggregate.ToString();
    }

    private static Reference<TAggregate> CreateReference(TAggregate aggregate)
    {
        return aggregate?.ToReference() ?? Reference<TAggregate>.Empty;
    }

    private static bool EqualOperator(Projection<TAggregate>? left, Reference? right)
    {
        return !(left is null ^ right is null)
            && (left is null || left.Aggregate.Equals(right));
    }

    private static bool NotEqualOperator(Projection<TAggregate>? left, Reference? right)
    {
        return !EqualOperator(left, right);
    }
}