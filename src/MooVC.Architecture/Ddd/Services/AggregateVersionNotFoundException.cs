namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using static System.String;
    using static MooVC.Architecture.Ddd.Services.Resources;

    [Serializable]
    public sealed class AggregateVersionNotFoundException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateVersionNotFoundException(Message context, VersionedReference<TAggregate> aggregate)
               : base(Format(
                   AggregateVersionNotFoundExceptionMessage,
                   aggregate.Id,
                   aggregate.Type.Name,
                   aggregate.Version))
        {
            Aggregate = aggregate;
            Context = context;
        }

        public AggregateVersionNotFoundException(Message context, Guid aggregateId, SignedVersion version)
            : this(context, new VersionedReference<TAggregate>(aggregateId, version))
        {
        }

        private AggregateVersionNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.TryGetVersionedReference<TAggregate>(nameof(Aggregate));
            Context = info.GetValue<Message>(nameof(Context));
        }

        public VersionedReference<TAggregate> Aggregate { get; }

        public Message Context { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddVersionedReference(nameof(Aggregate), Aggregate);
            info.AddValue(nameof(Context), Context);
        }
    }
}