namespace MooVC.Architecture.Ddd;

using System;
using System.Runtime.Serialization;
using MooVC.Serialization;

public abstract partial class AggregateRoot
{
    [Serializable]
    private protected readonly struct AggregateState
        : ISerializable
    {
        public AggregateState(Sequence persisted)
            : this(persisted, persisted)
        {
        }

        public AggregateState(Sequence current, Sequence persisted)
        {
            Current = current;
            Persisted = persisted;
        }

        private AggregateState(SerializationInfo info, StreamingContext context)
        {
            Persisted = info.TryGetValue(nameof(Persisted), defaultValue: Sequence.Empty);
            Current = info.TryGetValue(nameof(Current), defaultValue: Persisted);
        }

        public Sequence Current { get; }

        public bool HasUncommittedChanges => !(Current.IsEmpty || Current == Persisted);

        public Sequence Persisted { get; }

        public AggregateState Commit()
        {
            return new AggregateState(Current);
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _ = info.TryAddValue(nameof(Current), Current, defaultValue: Persisted);
            _ = info.TryAddValue(nameof(Persisted), Persisted, defaultValue: Sequence.Empty);
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