namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using static Resources;

    [Serializable]
    public sealed class AggregateConflictDetectedException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateConflictDetectedException(
               Guid aggregateId,
               ulong receivedVersion)
               : base(string.Format(
                   AggregateConflictDetectedExceptionNoExistingEntryMessage,
                   aggregateId,
                   typeof(TAggregate).Name,
                   receivedVersion))
        {
            AggregateId = aggregateId;
            ReceivedVersion = receivedVersion;
        }

        public AggregateConflictDetectedException(
            Guid aggregateId,
            ulong persistedVersion,
            ulong receivedVersion)
            : base(string.Format(
                AggregateConflictDetectedExceptionExistingEntryMessage,
                aggregateId,
                typeof(TAggregate).Name,
                receivedVersion,
                persistedVersion))
        {
            AggregateId = aggregateId;
            PersistedVersion = persistedVersion;
            ReceivedVersion = receivedVersion;
        }

        public Guid AggregateId { get; }

        public ulong ExpectedVersion => PersistedVersion + 1;

        public ulong PersistedVersion { get; }

        public ulong ReceivedVersion { get; }
    }
}