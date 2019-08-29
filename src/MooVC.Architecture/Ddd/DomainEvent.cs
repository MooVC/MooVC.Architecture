namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public abstract class DomainEvent
        : Message
    {
        protected DomainEvent(Message context, AggregateRoot aggregate)
            : base(context)
        {
            Aggregate = aggregate.ToReference();
        }

        protected DomainEvent(Message context, IReference aggregate)
            : base(context)
        {
            Aggregate = aggregate;
        }

        protected DomainEvent(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = (IReference)info.GetValue(nameof(Aggregate), typeof(IReference));
        }

        public IReference Aggregate { get; }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Aggregate), Aggregate);
        }
    }
}