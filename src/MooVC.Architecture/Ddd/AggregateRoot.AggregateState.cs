namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Serialization;

    public abstract partial class AggregateRoot
    {
        [Serializable]
        private protected readonly struct AggregateState
            : ISerializable
        {
            public AggregateState(SignedVersion persisted)
                : this(persisted, persisted)
            {
            }

            public AggregateState(SignedVersion current, SignedVersion persisted)
            {
                Current = current;
                Persisted = persisted;
            }

            private AggregateState(SerializationInfo info, StreamingContext context)
            {
                Persisted = info.TryGetValue(nameof(Persisted), defaultValue: SignedVersion.Empty);
                Current = info.TryGetValue(nameof(Current), defaultValue: Persisted);
            }

            public SignedVersion Current { get; }

            public bool HasUncommittedChanges => !(Current.IsEmpty || Current == Persisted);

            public SignedVersion Persisted { get; }

            public AggregateState Commit()
            {
                return new AggregateState(Current);
            }

            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                _ = info.TryAddValue(nameof(Current), Current, defaultValue: Persisted);
                _ = info.TryAddValue(nameof(Persisted), Persisted, defaultValue: SignedVersion.Empty);
            }

            public AggregateState Increment()
            {
                return new AggregateState(Persisted.Next(), Persisted);
            }

            public AggregateState Rollback()
            {
                return Current.IsNew
                    ? this
                    : new AggregateState(Persisted);
            }
        }
    }
}