namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using static MooVC.Ensure;
    using static Resources;

    [Serializable]
    public abstract class Reference
        : Value
    {
        private protected Reference(Guid id, ulong version)
        {
            ArgumentIsAcceptable(
                version, 
                nameof(version), 
                value => value >= AggregateRoot.DefaultVersion || id == Guid.Empty, 
                string.Format(GenericVersionInvalid, AggregateRoot.DefaultVersion));
            
            Id = id;
            Version = version;
        }

        private protected Reference(AggregateRoot aggregate)
        {
            Id = aggregate.Id;
            Version = aggregate.Version;
        }

        protected Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Id = (Guid)info.GetValue(nameof(Id), typeof(Guid));
            Version = (ulong)info.GetValue(nameof(Version), typeof(ulong));
        }

        public Guid Id { get; }

        public bool IsEmpty => Id == Guid.Empty;

        public abstract Type Type { get; }

        public ulong Version { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Version), Version);
        }

        public bool IsMatch(AggregateRoot aggregate)
        {
            return Type == aggregate?.GetType()
                ? Id == aggregate.Id && Version == aggregate.Version
                : false;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Version;
        }
    }
}