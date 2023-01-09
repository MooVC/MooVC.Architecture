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
        public AggregateState(SignedVersion persisted)
            : this(persisted, persisted)
        {
        }

        public AggregateState(SignedVersion current, SignedVersion persisted)
        {
            Current = current;
            Persisted = persisted;
        }

        /// <summary>
        /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
        /// of the <see cref="Paging"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
        /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
        [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
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

        /// <summary>
        /// Populates the specified <see cref="SerializationInfo"/> object with the data needed to serialize the current instance
        /// of the <see cref="Paging"/> class.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> object that will be populated with data.</param>
        /// <param name="context">The destination (see <see cref="StreamingContext"/>) for the serialization operation.</param>
        [Obsolete(@"Slated for removal in v11 as part of Microsoft's BinaryFormatter Obsoletion Strategy.
                       (see: https://github.com/dotnet/designs/blob/main/accepted/2020/better-obsoletion/binaryformatter-obsoletion.md)")]
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