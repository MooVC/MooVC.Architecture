namespace MooVC.Architecture.Ddd;

using System;
using MooVC.Threading;

/// <summary>
/// Represents an abstract base class for entities, providing a generic mechanism for entity identification and comparison in the context of its
/// containing <see cref="AggregateRoot"/>.
/// </summary>
/// <typeparam name="T">The type of the identifier for the entity. Must not be null.</typeparam>
public abstract class Entity<T>
    : ICoordinatable,
      IEquatable<Entity<T>>
    where T : notnull
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class with the specified identifier.
    /// </summary>
    /// <param name="id">The local identifier of the entity in the context of its containing <see cref="AggregateRoot"/>.</param>
    protected Entity(T id)
    {
        Id = id;
    }

    /// <summary>
    /// Gets the identifier of the entity.
    /// </summary>
    public T Id { get; }

    /// <summary>
    /// Determines whether two specified instances of <see cref="Entity{T}"/> are equal.
    /// </summary>
    /// <param name="first">The first entity to compare.</param>
    /// <param name="second">The second entity to compare.</param>
    /// <returns><c>true</c> if the first entity is the same as the second entity; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Entity<T>? first, Entity<T>? second)
    {
        return EqualOperator(first, second);
    }

    /// <summary>
    /// Determines whether two specified instances of <see cref="Entity{T}"/> are not equal.
    /// </summary>
    /// <param name="first">The first entity to compare.</param>
    /// <param name="second">The second entity to compare.</param>
    /// <returns><c>true</c> if the first entity is not the same as the second entity; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Entity<T>? first, Entity<T>? second)
    {
        return NotEqualOperator(first, second);
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current entity.
    /// </summary>
    /// <param name="other">The object to compare with the current entity.</param>
    /// <returns><c>true</c> if the specified object is equal to the current entity; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? other)
    {
        if (other is Entity<T> entity)
        {
            return Equals(entity);
        }

        return false;
    }

    /// <summary>
    /// Determines whether the specified entity is equal to the current entity.
    /// </summary>
    /// <param name="other">The entity to compare with the current entity.</param>
    /// <returns><c>true</c> if the specified entity is equal to the current entity; otherwise, <c>false</c>.</returns>
    public virtual bool Equals(Entity<T>? other)
    {
        return other is not null
            && Id.Equals(other.Id)
            && GetType() == other.GetType();
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current entity.</returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    /// Returns a string that represents the current <see cref="Entity{T}"/>.
    /// </summary>
    /// <returns>A string representation of the <see cref="Entity{T}"/>.</returns>
    public override string ToString()
    {
        return $"{GetType().FullName} [{Id:D}]";
    }

    /// <summary>
    /// Retrieves a key for this instance, facilitating the use of <see cref="ICoordinatable"/>.
    /// </summary>
    /// <returns>A string representation of the unique identifier for the entity.</returns>
    string ICoordinatable.GetKey()
    {
        return ToString();
    }

    /// <summary>
    /// Implements the equality operator by comparing two entities for equality.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns><c>true</c> if both entities are null or equal; otherwise, <c>false</c>.</returns>
    private static bool EqualOperator(Entity<T>? left, Entity<T>? right)
    {
        return !(left is null ^ right is null)
            && (left is null || left.Equals(right));
    }

    /// <summary>
    /// Implements the inequality operator by comparing two entities for inequality.
    /// </summary>
    /// <param name="left">The first entity to compare.</param>
    /// <param name="right">The second entity to compare.</param>
    /// <returns><c>true</c> if the entities are not equal; otherwise, <c>false</c>.</returns>
    private static bool NotEqualOperator(Entity<T>? left, Entity<T>? right)
    {
        return !EqualOperator(left, right);
    }
}