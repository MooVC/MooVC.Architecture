namespace MooVC.Architecture;

using System;
using System.Runtime.Serialization;
using MooVC.Serialization;
using MooVC.Threading;

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

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
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

    /// <summary>
    /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
    /// of the <see cref="Paging"/> class.
    /// </summary>
    /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
    [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Id), Id);
    }

    public override string ToString()
    {
        return $"{GetType().FullName} [{Id:D}]";
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