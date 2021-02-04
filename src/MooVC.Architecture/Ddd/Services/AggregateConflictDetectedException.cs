namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
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

        private protected AggregateConflictDetectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.TryGetReference(nameof(Aggregate));
            PersistedVersion = info.TryGetValue(nameof(PersistedVersion), defaultValue: SignedVersion.Empty);
            ReceivedVersion = info.TryGetValue(nameof(ReceivedVersion), defaultValue: SignedVersion.Empty);
        }

        public Reference Aggregate { get; }

        public SignedVersion PersistedVersion { get; }

        public SignedVersion ReceivedVersion { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Aggregate), Aggregate);
            _ = info.TryAddValue(nameof(PersistedVersion), PersistedVersion, defaultValue: SignedVersion.Empty);
            _ = info.TryAddValue(nameof(ReceivedVersion), ReceivedVersion, defaultValue: SignedVersion.Empty);
        }
    }
}