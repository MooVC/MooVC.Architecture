namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Serialization;
    using static System.String;
    using static MooVC.Architecture.Ddd.Resources;

    [Serializable]
    public sealed class AggregateHasUncommittedChangesException
        : ArgumentException
    {
        internal AggregateHasUncommittedChangesException(VersionedReference aggregate)
            : base(Format(
                AggregateHasUncommittedChangesExceptionMessage,
                aggregate.Id,
                aggregate.Version,
                aggregate.Type.Name))
        {
            Aggregate = aggregate;
        }

        private AggregateHasUncommittedChangesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Aggregate = info.TryGetVersionedReference(nameof(Aggregate));
        }

        public VersionedReference Aggregate { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            _ = info.TryAddVersionedReference(nameof(Aggregate), Aggregate);
        }
    }
}