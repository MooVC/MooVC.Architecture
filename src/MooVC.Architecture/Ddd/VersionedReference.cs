namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public abstract class VersionedReference
        : Reference
    {
        protected VersionedReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Version = (ulong)info.GetValue(nameof(Version), typeof(ulong));
        }

        private protected VersionedReference(Guid id, ulong version)
            : base(id)
        {
            ArgumentIsAcceptable(
                version,
                nameof(version),
                value => value >= AggregateRoot.DefaultVersion || id == Guid.Empty,
                string.Format(GenericVersionInvalid, AggregateRoot.DefaultVersion));

            Version = version;
        }

        private protected VersionedReference(AggregateRoot aggregate)
            : base(aggregate)
        {
            Version = aggregate.Version;
        }

        public ulong Version { get; }

        public static bool operator ==(VersionedReference first, VersionedReference second)
        {
            return EqualOperator(first, second);
        }

        public static bool operator !=(VersionedReference first, VersionedReference second)
        {
            return NotEqualOperator(first, second);
        }

        public override bool Equals(object other)
        {
            return other is VersionedReference value
                ? Id == value.Id && Type == value.Type && Version == value.Version
                : false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Version), Version);
        }

        public override bool IsMatch(AggregateRoot aggregate)
        {
            return base.IsMatch(aggregate)
                && Version == aggregate.Version;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return base
                .GetAtomicValues()
                .Union(new object[] { Version });
        }

        private static bool EqualOperator(VersionedReference left, VersionedReference right)
        {
            return left is null ^ right is null
                ? false
                : left is null || left.Equals(right);
        }

        private static bool NotEqualOperator(VersionedReference left, VersionedReference right)
        {
            return !EqualOperator(left, right);
        }
    }
}