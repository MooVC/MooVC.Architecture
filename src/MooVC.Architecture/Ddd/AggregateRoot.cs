namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.Serialization;
    using MooVC.Serialization;
    using static MooVC.Architecture.Ddd.Reference;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract partial class AggregateRoot
        : Entity<Guid>
    {
        protected AggregateRoot(Guid id)
            : base(id)
        {
            ArgumentIsAcceptable(
                id,
                nameof(id),
                value => value != Guid.Empty,
                AggregateRootIdRequired);

            State = new AggregateState(new SignedVersion(), SignedVersion.Empty);
        }

        protected AggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            State = info.GetInternalValue<AggregateState>(nameof(State));
        }

        public event EventHandler? ChangesMarkedAsCommitted;

        public event EventHandler? ChangesMarkedAsUncommitted;

        public event EventHandler? ChangesRolledBack;

        public SignedVersion Version => State.Current;

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

        public static implicit operator SignedVersion(AggregateRoot? aggregate)
        {
            return aggregate?.Version ?? SignedVersion.Empty;
        }

        public override bool Equals(object? other)
        {
            return base.Equals(other) &&
                other is AggregateRoot aggregate
                && Version.Equals(aggregate.Version);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Version.GetHashCode();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddInternalValue(nameof(State), State);
        }

        public virtual void MarkChangesAsCommitted()
        {
            if (HasUncommittedChanges)
            {
                State = State.Commit();

                OnChangesMarkedAsCommitted();
            }
        }

        protected virtual void OnChangesMarkedAsCommitted(EventArgs? args = default)
        {
            ChangesMarkedAsCommitted?.Invoke(this, args ?? EventArgs.Empty);
        }

        protected virtual void OnChangesMarkedAsUncommitted(EventArgs? args = default)
        {
            ChangesMarkedAsUncommitted?.Invoke(this, args ?? EventArgs.Empty);
        }

        protected virtual void OnChangesRolledBack(EventArgs? args = default)
        {
            ChangesRolledBack?.Invoke(this, args ?? EventArgs.Empty);
        }

        protected virtual void MarkChangesAsUncommitted()
        {
            if (!HasUncommittedChanges)
            {
                State = State.Increment();

                OnChangesMarkedAsUncommitted();
            }
        }

        protected virtual void RollbackUncommittedChanges()
        {
            if (HasUncommittedChanges)
            {
                State = State.Rollback();

                OnChangesRolledBack();
            }
        }
    }
}