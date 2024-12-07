﻿namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MooVC.Collections.Generic;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Resources;

[Serializable]
public abstract partial class EventCentricAggregateRoot
    : AggregateRoot
{
    private readonly List<DomainEvent> changes;

    protected EventCentricAggregateRoot(Guid id)
        : base(id)
    {
        changes = new List<DomainEvent>();
    }

    protected EventCentricAggregateRoot(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        changes = info.TryGetInternalValue(nameof(changes), defaultValue: new List<DomainEvent>());
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);

        _ = info.TryAddInternalValue(nameof(changes), changes, predicate: _ => changes.Any());
    }

    public IEnumerable<DomainEvent> GetUncommittedChanges()
    {
        return changes.ToArray();
    }

    public void LoadFromHistory(IEnumerable<DomainEvent> history)
    {
        if (history.Any())
        {
            if (changes.Any())
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

            if (mismatch is { })
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

            if (handler is { })
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
            if (isNew && @event is { })
            {
                _ = changes.Remove(@event);

                if (!changes.Any())
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

    protected override void OnChangesMarkedAsCommitted(EventArgs? args = default)
    {
        base.OnChangesMarkedAsCommitted(new ChangesMarkedAsCommittedEventArgs(changes));
    }

    protected sealed override void RollbackUncommittedChanges()
    {
        throw new InvalidOperationException(EventCentricAggregateRootStateChangesDenied);
    }
}