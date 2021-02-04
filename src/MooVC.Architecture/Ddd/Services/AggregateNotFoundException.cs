namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using static System.String;
    using static MooVC.Architecture.Ddd.Services.Resources;

    [Serializable]
    public sealed class AggregateNotFoundException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateNotFoundException(Message context, Reference<TAggregate> aggregate)
            : base(Format(
                AggregateNotFoundExceptionMessage,
                aggregate.Id,
                aggregate.Type.Name))
        {
            Aggregate = aggregate;
            Context = context;
        }

        public AggregateNotFoundException(Message context, Guid aggregateId)
            : this(context, aggregateId.ToReference<TAggregate>())
        {
        }

        private AggregateNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.TryGetReference<TAggregate>(nameof(Aggregate));
            Context = info.GetValue<Message>(nameof(Message));
        }

        public Reference<TAggregate> Aggregate { get; }

        public Message Context { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddReference(nameof(Aggregate), Aggregate);
            info.AddValue(nameof(Message), Message);
        }
    }
}