namespace MooVC.Architecture.Ddd.Services
{
    using System;

    [Serializable]
    public sealed class AggregteConflictDetectedException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        internal AggregteConflictDetectedException(
            Guid aggregateId,
            ulong expectedVersion,
            ulong persistedVersion)
            : base(string.Format(
                Resources.AggregteConflictDetectedExceptionMessage,
                aggregateId,
                typeof(TAggregate).Name,
                expectedVersion,
                persistedVersion))
        {
            AggregateId = aggregateId;
            ExpectedVersion = expectedVersion;
            PersistedVersion = persistedVersion;
        }

        public Guid AggregateId { get; }

        public ulong ExpectedVersion { get; }

        public ulong PersistedVersion { get; }
    }
}