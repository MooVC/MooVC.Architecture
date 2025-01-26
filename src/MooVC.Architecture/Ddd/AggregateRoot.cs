namespace MooVC.Architecture.Ddd;

using Ardalis.GuardClauses;
using static MooVC.Architecture.Ddd.Reference;
using static MooVC.Architecture.Ddd.Resources;

public abstract partial class AggregateRoot
    : Entity<Guid>
{
    protected AggregateRoot(Guid id)
        : base(id)
    {
        _ = Guard.Against.Default(id, message: AggregateRootIdRequired);

        State = new AggregateState(new Sequence(), Sequence.Empty);
    }

    public Sequence Version => State.Current;

    public bool HasUncommittedChanges => State.HasUncommittedChanges;

    private protected AggregateState State { get; set; }

    public static implicit operator Guid(AggregateRoot? aggregate)
    {
        return aggregate?.Id ?? Guid.Empty;
    }

    public static implicit operator Reference(AggregateRoot? aggregate)
    {
        if (aggregate is { })
        {
            return Create(aggregate);
        }

        return Reference<AggregateRoot>.Empty;
    }

    public static implicit operator Sequence(AggregateRoot? aggregate)
    {
        return aggregate?.Version ?? Sequence.Empty;
    }

    public override bool Equals(object? other)
    {
        return base.Equals(other) && other is AggregateRoot aggregate && Version.Equals(aggregate.Version);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ Version.GetHashCode();
    }

    public virtual void MarkChangesAsCommitted()
    {
        if (HasUncommittedChanges)
        {
            State = State.Commit();
        }
    }

    protected virtual void MarkChangesAsUncommitted()
    {
        if (!HasUncommittedChanges)
        {
            State = State.Increment();
        }
    }

    protected virtual void RollbackUncommittedChanges()
    {
        if (HasUncommittedChanges)
        {
            State = State.Rollback();
        }
    }
}