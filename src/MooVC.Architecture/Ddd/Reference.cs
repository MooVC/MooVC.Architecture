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
            new Lazy<Reference<TAggregate>>(() => new Reference<TAggregate>(Guid.Empty, AggregateRoot.DefaultVersion));

        public Reference(Guid id, ulong version)
        {
            Id = id;
            Version = version;
        }

        public Reference(TAggregate aggregate)
        {
            Id = aggregate.Id;
            Version = aggregate.Version;
        }

        private Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Id = (Guid)info.GetValue(nameof(Id), typeof(Guid));
            Version = (ulong)info.GetValue(nameof(Version), typeof(ulong));
        }

        public static IReference Empty => ActualEmpty.Value;

        public Guid Id { get; }

        public bool IsEmpty => Id == Guid.Empty;

        public Type Type => typeof(TAggregate);

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