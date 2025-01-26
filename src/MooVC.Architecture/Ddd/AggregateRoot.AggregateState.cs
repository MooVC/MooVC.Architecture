namespace MooVC.Architecture.Ddd;

using System.Runtime.Serialization;

public abstract partial class AggregateRoot
{
    private protected readonly struct AggregateState
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

        public Sequence Current { get; }

        public bool HasUncommittedChanges => !(Current.IsEmpty || Current == Persisted);

        public Sequence Persisted { get; }

        public AggregateState Commit()
        {
            return new AggregateState(Current);
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