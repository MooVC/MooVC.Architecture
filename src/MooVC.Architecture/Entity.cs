namespace MooVC.Architecture;

using System;
using System.Runtime.Serialization;
using MooVC.Serialization;
using MooVC.Threading;
using static System.String;

[Serializable]
public abstract class Entity<T>
    : ISerializable,
      ICoordinatable<T>,
      IEquatable<Entity<T>>
    where T : notnull
{
    protected Entity(T id)
    {
        Id = id;
    }

    protected Entity(SerializationInfo info, StreamingContext context)
    {
        Id = info.GetValue<T>(nameof(Id));
    }

    public T Id { get; }

    public static bool operator ==(Entity<T>? first, Entity<T>? second)
    {
        return EqualOperator(first, second);
    }

    public static bool operator !=(Entity<T>? first, Entity<T>? second)
    {
        return NotEqualOperator(first, second);
    }

    public override bool Equals(object? other)
    {
        if (other is Entity<T> entity)
        {
            return Equals(entity);
        }

        return false;
    }

    public virtual bool Equals(Entity<T>? other)
    {
        return other is { }
            && Id.Equals(other.Id)
            && GetType() == other.GetType();
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Id), Id);
    }

    public override string ToString()
    {
        return Id.ToString() ?? Empty;
    }

    T ICoordinatable<T>.GetKey()
    {
        return Id;
    }

    private static bool EqualOperator(Entity<T>? left, Entity<T>? right)
    {
        return !(left is null ^ right is null)
            && (left is null || left.Equals(right));
    }

    private static bool NotEqualOperator(Entity<T>? left, Entity<T>? right)
    {
        return !EqualOperator(left, right);
    }
}