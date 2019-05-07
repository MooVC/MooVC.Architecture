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
        public Reference(Guid id)
        {
            Id = id;
        }

        public Reference(TAggregate aggregate)
        {
            Id = aggregate.Id;
        }

        private Reference(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Id = (Guid)info.GetValue(nameof(Id), typeof(Guid));
        }

        public Guid Id { get; }

        public Type Type => typeof(TAggregate);

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Id), Id);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Id;
        }
    }
}