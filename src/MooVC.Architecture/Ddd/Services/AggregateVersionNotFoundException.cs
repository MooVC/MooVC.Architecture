namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using static System.String;
    using static MooVC.Architecture.Ddd.Services.Resources;

    [Serializable]
    public sealed class AggregateVersionNotFoundException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateVersionNotFoundException(Message context, Guid aggregateId, SignedVersion version)
            : base(Format(
                AggregateVersionNotFoundExceptionMessage,
                aggregateId,
                typeof(TAggregate).Name,
                version))
        {
            Aggregate = new VersionedReference<TAggregate>(aggregateId, version);
            Context = context;
        }

        public VersionedReference Aggregate { get; }

        public Message Context { get; }
    }
}