namespace MooVC.Architecture.Ddd.Services
{
    using System;

    [Serializable]
    public sealed class AggregateVersionNotFoundException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        internal AggregateVersionNotFoundException(Message context, Guid aggregateId, ulong version)
            : base(string.Format(
                Resources.AggregateVersionNotFoundExceptionMessage,
                aggregateId,
                typeof(TAggregate).Name,
                version))
        {
            AggregateId = aggregateId;
            Context = context;
            Version = version;
        }

        public Guid AggregateId { get; }

        public Message Context { get; }

        public ulong Version { get; }
    }
}