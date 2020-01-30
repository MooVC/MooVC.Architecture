namespace MooVC.Architecture.Ddd
{
    using System;
    using static System.String;
    using static Resources;

    [Serializable]
    public sealed class AggregateHasUncommittedChangesException
        : ArgumentException
    {
        internal AggregateHasUncommittedChangesException(VersionedReference aggregate)
            : base(Format(
                AggregateHasUncommittedChangesExceptionMessage,
                aggregate.Id,
                aggregate.Type.Name,
                aggregate.Version))
        {
            Aggregate = aggregate;
        }

        public VersionedReference Aggregate { get; }
    }
}