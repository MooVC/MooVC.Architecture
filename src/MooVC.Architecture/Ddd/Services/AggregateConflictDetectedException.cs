namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using static System.String;
    using static Resources;

    [Serializable]
    public sealed class AggregateConflictDetectedException<TAggregate>
        : ArgumentException
        where TAggregate : AggregateRoot
    {
        public AggregateConflictDetectedException(
               Guid aggregateId,
               SignedVersion receivedVersion)
               : base(Format(
                   AggregateConflictDetectedExceptionNoExistingEntryMessage,
                   aggregateId,
                   typeof(TAggregate).Name,
                   receivedVersion))
        {
            AggregateId = aggregateId;
            PersistedVersion = SignedVersion.Empty;
            ReceivedVersion = receivedVersion;
        }

        public AggregateConflictDetectedException(
            Guid aggregateId,
            SignedVersion persistedVersion,
            SignedVersion receivedVersion)
            : base(Format(
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

        public SignedVersion PersistedVersion { get; }

        public SignedVersion ReceivedVersion { get; }
    }
}