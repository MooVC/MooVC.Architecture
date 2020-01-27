namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using MooVC.Serialization;

    public abstract partial class AggregateRoot
    {
        [Serializable]
        private protected readonly struct AggregateState
            : ISerializable
        {
            public AggregateState(SignedVersion current)
                : this(current, current)
            {
            }

            public AggregateState(SignedVersion current, SignedVersion persisted)
            {
                Current = current ?? new SignedVersion();
                Persisted = persisted;
            }

            private AggregateState(SerializationInfo info, StreamingContext context)
            {
                Persisted = info.GetValue<SignedVersion>(nameof(Persisted));
                Current = info.GetValue<SignedVersion>(nameof(Current)) ?? Persisted;
            }

            public SignedVersion Current { get; }

            public bool HasUncommittedChanges => !(IsPersisted && Current == Persisted);

            public bool IsPersisted => Persisted is { };

            public SignedVersion Persisted { get; }

            public AggregateState Commit()
            {
                return new AggregateState(Current, Current);
            }

            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                SignedVersion current = Current == Persisted
                    ? default
                    : Current;

                info.AddValue(nameof(Current), current);
                info.AddValue(nameof(Persisted), Persisted);
            }

            public AggregateState Increment()
            {
                return new AggregateState(new SignedVersion(Persisted), Persisted);
            }

            public AggregateState Rollback()
            {
                return new AggregateState(Persisted, Persisted);
            }
        }
    }
}
