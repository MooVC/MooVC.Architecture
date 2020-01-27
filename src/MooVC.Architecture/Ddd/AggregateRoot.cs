namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Serialization;
    using static MooVC.Ensure;
    using static Resources;

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
                GenericIdInvalid);

            State = new AggregateState(new SignedVersion(), null);
        }

        protected AggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            State = info.GetInternalValue<AggregateState>(nameof(State));
        }

        public event EventHandler ChangesMarkedAsCommitted;

        public event EventHandler ChangesMarkedAsUncommitted;

        public event EventHandler ChangesRolledBack;

        public SignedVersion Version => State.Current;

        public bool HasUncommittedChanges => State.HasUncommittedChanges;

        private protected AggregateState State { get; set; }

        public override bool Equals(object other)
        {
            return base.Equals(other) &&
                other is AggregateRoot aggregate
                && Version.Equals(aggregate.Version);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ Version.GetHashCode();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
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

        protected virtual void OnChangesMarkedAsCommitted(EventArgs e = null)
        {
            ChangesMarkedAsCommitted?.Invoke(this, e ?? EventArgs.Empty);
        }

        protected virtual void OnChangesMarkedAsUncommitted(EventArgs e = null)
        {
            ChangesMarkedAsUncommitted?.Invoke(this, e ?? EventArgs.Empty);
        }

        protected virtual void OnChangesRolledBack(EventArgs e = null)
        {
            ChangesRolledBack?.Invoke(this, e ?? EventArgs.Empty);
        }

        private protected virtual void MarkChangesAsUncommitted()
        {
            if (!HasUncommittedChanges)
            {
                State = State.Increment();

                OnChangesMarkedAsUncommitted();
            }
        }

        private protected virtual void RollbackUncommittedChanges()
        {
            if (HasUncommittedChanges)
            {
                State = State.Rollback();

                OnChangesRolledBack();
            }
        }
    }
}