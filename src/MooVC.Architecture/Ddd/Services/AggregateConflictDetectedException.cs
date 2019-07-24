namespace MooVC.Architecture.Ddd.Services
{
    using System;

    [Serializable]
    public sealed class AggregateConflictDetectedException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        internal AggregateConflictDetectedException(
            Guid aggregateId,
            ulong expectedVersion,
            ulong persistedVersion)
            : base(string.Format(
                Resources.AggregateConflictDetectedExceptionMessage,
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