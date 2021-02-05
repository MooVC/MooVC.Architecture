namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Serialization;
    using MooVC.Serialization;
    using static System.String;
    using static MooVC.Architecture.Ddd.Ensure;
    using static MooVC.Architecture.Ddd.Services.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract class AggregateConflictDetectedException
        : ArgumentException
    {
        private protected AggregateConflictDetectedException(Reference aggregate, SignedVersion received)
            : base(FormatMessage(aggregate, received))
        {
            Aggregate = aggregate;
            Persisted = SignedVersion.Empty;
            Received = received;
        }

        private protected AggregateConflictDetectedException(Reference aggregate, SignedVersion persisted, SignedVersion received)
            : base(FormatMessage(aggregate, received, persisted: persisted, persistedRequired: true))
        {
            Aggregate = aggregate;
            Persisted = persisted;
            Received = received;
        }

        private protected AggregateConflictDetectedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.TryGetReference(nameof(Aggregate));
            Persisted = info.TryGetValue(nameof(Persisted), defaultValue: SignedVersion.Empty);
            Received = info.TryGetValue(nameof(Received), defaultValue: SignedVersion.Empty);
        }

        public Reference Aggregate { get; }

        public SignedVersion Persisted { get; }

        public SignedVersion Received { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(Aggregate), Aggregate);
            _ = info.TryAddValue(nameof(Persisted), Persisted, defaultValue: SignedVersion.Empty);
            _ = info.TryAddValue(nameof(Received), Received, defaultValue: SignedVersion.Empty);
        }

        private static string FormatMessage(
            Reference aggregate,
            SignedVersion received,
            SignedVersion? persisted = default,
            bool persistedRequired = false)
        {
            ReferenceIsNotEmpty(aggregate, nameof(aggregate), AggregateConflictDetectedExceptionAggregateRequired);
            ArgumentNotNull(received, nameof(received), AggregateConflictDetectedExceptionReceivedRequired);

            if (persistedRequired)
            {
                ArgumentNotNull(persisted, nameof(persisted), AggregateConflictDetectedExceptionPersistedRequired);

                return Format(
                    AggregateConflictDetectedExceptionExistingEntryMessage,
                    aggregate.Id,
                    aggregate.Type.Name,
                    received,
                    persisted);
            }

            return Format(
                AggregateConflictDetectedExceptionNoExistingEntryMessage,
                aggregate.Id,
                aggregate.Type.Name,
                received);
        }
    }
}