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
                aggregate.Version,
                aggregate.Type.Name))
        {
            Aggregate = aggregate;
        }

        public VersionedReference Aggregate { get; }
    }
}