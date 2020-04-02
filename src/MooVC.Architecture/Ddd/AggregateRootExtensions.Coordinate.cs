namespace MooVC.Architecture.Ddd
{
    using System;
    using static MooVC.Ensure;
    using static Resources;

    public static partial class AggregateRootExtensions
    {
        public static void Coordinate(this AggregateRoot aggregate, Action operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), AggregateRootExtensionsCoordinateAggregateRequired);

            aggregate.GetType().Coordinate(
                aggregate.Id,
                operation,
                timeout: timeout);
        }
    }
}