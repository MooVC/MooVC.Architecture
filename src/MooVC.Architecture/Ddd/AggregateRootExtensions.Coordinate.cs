namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    public static partial class AggregateRootExtensions
    {
        public static void Coordinate(this AggregateRoot aggregate, Action operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), AggregateRootExtensionsCoordinateAggregateRequired);

            aggregate
                .GetType()
                .Coordinate(
                    aggregate.Id,
                    operation,
                    timeout: timeout);
        }

        public static async Task CoordinateAsync(this AggregateRoot aggregate, Func<Task> operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), AggregateRootExtensionsCoordinateAggregateRequired);

            await aggregate
                .GetType()
                .CoordinateAsync(
                    aggregate.Id,
                    operation,
                    timeout: timeout);
        }
    }
}