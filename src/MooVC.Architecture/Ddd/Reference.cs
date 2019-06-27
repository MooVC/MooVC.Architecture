namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public sealed class Reference<TAggregate>
        : Value, IReference
        where TAggregate : AggregateRoot
    {
        private static readonly Lazy<Reference<TAggregate>> ActualEmpty = 
            new Lazy<Reference<TAggregate>>(() => new Reference<TAggregate>(Guid.Empty));

        public Reference(Guid id, ulong? version = null)
        {
            Id = id;
            Version = version;
        }

        public Reference(TAggregate aggregate, bool enforceVersion = false)
        {
            Id = aggregate.Id;
            
            if (enforceVersion)
            {
                Version = aggregate.Version;
            }
        }

        private Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Id = (Guid)info.GetValue(nameof(Id), typeof(Guid));
            Version = (ulong?)info.GetValue(nameof(Version), typeof(ulong?));
        }

        public static Reference<TAggregate> Empty => ActualEmpty.Value;

        public Guid Id { get; }

        public bool IsEmpty => Id == Guid.Empty;

        public bool IsVersionSpecific => Version.HasValue;

        public Type Type => typeof(TAggregate);

        public ulong? Version { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Version), Version);
        }

        public bool IsMatch<TProposedAggregate>(TProposedAggregate aggregate)
            where TProposedAggregate : AggregateRoot
        {
            return Type == typeof(TProposedAggregate) 
                ? Id == aggregate.Id && (!IsVersionSpecific || Version.Value == aggregate.Version) 
                : false;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
            yield return Version;
        }
    }
}