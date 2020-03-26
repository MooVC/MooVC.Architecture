namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Serialization;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public class VersionedReference
        : Reference
    {
        internal VersionedReference(AggregateRoot aggregate)
            : base(aggregate)
        {
            Version = aggregate.Version;
        }

        internal VersionedReference(Guid id, Type type, SignedVersion version)
            : base(id, type)
        {
            ArgumentNotNull(
                version,
                nameof(version),
                VersionedReferenceVersionRequired);

            Version = version;
        }

        private protected VersionedReference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Version = info.TryGetValue(nameof(Version), defaultValue: SignedVersion.Empty);
        }

        public SignedVersion Version { get; }

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

            _ = info.TryAddValue(nameof(Version), Version, defaultValue: SignedVersion.Empty);
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