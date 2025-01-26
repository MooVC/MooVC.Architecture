namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MooVC.Collections.Generic;
using MooVC.Linq;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Resources;

public abstract partial class EventCentricAggregateRoot
    : AggregateRoot
{
    private readonly List<DomainEvent> changes;

    protected EventCentricAggregateRoot(Guid id)
        : base(id)
    {
        changes = [];
    }

    public IEnumerable<DomainEvent> GetUncommittedChanges()
    {
        return [.. changes];
    }

    public void LoadFromHistory(IEnumerable<DomainEvent> history)
    {
        if (history.Any())
        {
            if (changes.Count > 0)
            {
                throw new AggregateHasUncommittedChangesException(this);
            }

            IEnumerable<DomainEvent> sequence = history
                .OrderBy(@event => @event.Aggregate.Version)
                .ToArray();

            if (!sequence.SequenceEqual(history))
            {
                throw new AggregateEventSequenceUnorderedException(this, history);
            }

            Reference? mismatch = sequence
                .Where(@event => @event.Aggregate.Id != Id)
                .Select(@event => @event.Aggregate)
                .FirstOrDefault();

            if (mismatch is not null)
            {
                throw new AggregateEventMismatchException(this, mismatch);
            }

            Sequence startingVersion = sequence.First().Aggregate.Version;

            if (!(startingVersion.IsNext(Version) || (startingVersion.IsNew && Version.IsNew)))
            {
                throw new AggregateHistoryInvalidForStateException(this, sequence, startingVersion);
            }

            sequence.ForEach(@event => ApplyChange(() => @event, isNew: false));

            State = new AggregateState(sequence.Last().Aggregate.Version);
        }
    }

    public sealed override void MarkChangesAsCommitted()
    {
        if (HasUncommittedChanges)
        {
            base.MarkChangesAsCommitted();

            changes.Clear();
        }
    }

    protected void ApplyChange<TEvent>(Func<TEvent> change, Action<TEvent>? handler = default, bool isNew = true)
        where TEvent : DomainEvent
    {
        TEvent? @event = default;

        if (isNew && !HasUncommittedChanges)
        {
            base.MarkChangesAsUncommitted();
        }

        try
        {
            @event = change();

            handler ??= ResolveHandler<TEvent>(@event);

            if (handler is not null)
            {
                handler(@event);
            }

            if (isNew)
            {
                changes.Add(@event);
            }
        }
        catch
        {
            if (isNew && @event is not null)
            {
                _ = changes.Remove(@event);

                if (changes.Count == 0)
                {
                    base.RollbackUncommittedChanges();
                }
            }

            throw;
        }
    }

    protected sealed override void MarkChangesAsUncommitted()
    {
        throw new InvalidOperationException(EventCentricAggregateRootStateChangesDenied);
    }

    protected sealed override void RollbackUncommittedChanges()
    {
        throw new InvalidOperationException(EventCentricAggregateRootStateChangesDenied);
    }
}