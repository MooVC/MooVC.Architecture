namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public abstract class AggregateRoot 
        : Entity<Guid>
    {
        public const ulong DefaultVersion = 1;

        protected AggregateRoot(Guid id, ulong version = DefaultVersion)
            : base(id)
        {
            Version = version;
        }

        protected AggregateRoot(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Version = (ulong)info.GetValue(nameof(Version), typeof(ulong));
        }

        public ulong Version { get; private protected set; }

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

            info.AddValue(nameof(Version), Version);
        }

        public virtual void MarkChangesAsCommitted()
        {
            Version++;
        }
    }
}