namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public abstract class AggregateRoot 
        : Entity<Guid>
    {
        public const ulong DefaultVersion = 1;
        
        protected AggregateRoot(Guid id, ulong version = DefaultVersion)
            : base(id)
        {
            ArgumentIsAcceptable(
                id,
                nameof(id),
                value => value != Guid.Empty,
                GenericIdInvalid);

            ArgumentIsAcceptable(
                version,
                nameof(version),
                value => value >= DefaultVersion,
                string.Format(GenericVersionInvalid, DefaultVersion));

            Version = version;
        }

        protected AggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            HasUncommittedChanges = info.GetBoolean(nameof(HasUncommittedChanges));
            Version = (ulong)info.GetValue(nameof(Version), typeof(ulong));
        }

        public event EventHandler ChangesMarkedAsCommitted;

        public event EventHandler ChangesMarkedAsUncommitted;

        public event EventHandler ChangesRolledBack;

        public ulong Version { get; private protected set; }

        protected virtual bool HasUncommittedChanges { get; protected private set; } = true;

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

            info.AddValue(nameof(HasUncommittedChanges), HasUncommittedChanges);
            info.AddValue(nameof(Version), Version);
        }

        public virtual void MarkChangesAsCommitted()
        {
            if (HasUncommittedChanges)
            {
                HasUncommittedChanges = false;

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
                Version++;
                HasUncommittedChanges = true;

                OnChangesMarkedAsUncommitted();
            }
        }

        private protected virtual void RollbackUncommittedChanges()
        {
            if (HasUncommittedChanges)
            {
                Version--;
                HasUncommittedChanges = false;

                OnChangesRolledBack();
            }
        }
    }
}