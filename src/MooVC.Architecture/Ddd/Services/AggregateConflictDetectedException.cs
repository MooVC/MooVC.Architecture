namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using static System.String;
    using static MooVC.Architecture.Ddd.Services.Resources;

    [Serializable]
    public abstract class AggregateConflictDetectedException
        : ArgumentException
    {
        private protected AggregateConflictDetectedException(
               Reference aggregate,
               SignedVersion received)
               : base(Format(
                   AggregateConflictDetectedExceptionNoExistingEntryMessage,
                   aggregate.Id,
                   aggregate.Type.Name,
                   received))
        {
            Aggregate = aggregate;
            PersistedVersion = SignedVersion.Empty;
            ReceivedVersion = received;
        }

        private protected AggregateConflictDetectedException(
            Reference aggregate,
            SignedVersion persistedVersion,
            SignedVersion receivedVersion)
            : base(Format(
                AggregateConflictDetectedExceptionExistingEntryMessage,
                aggregate.Id,
                aggregate.Type.Name,
                receivedVersion,
                persistedVersion))
        {
            Aggregate = aggregate;
            PersistedVersion = persistedVersion;
            ReceivedVersion = receivedVersion;
        }

        public Reference Aggregate { get; }

        public SignedVersion PersistedVersion { get; }

        public SignedVersion ReceivedVersion { get; }
    }
}