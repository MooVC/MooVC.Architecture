namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Ddd.Resources;
    using static MooVC.Ensure;

    public static partial class AggregateRootExtensions
    {
        public static async Task CoordinateAsync(
            this AggregateRoot aggregate,
            Func<Task> operation,
            CancellationToken? cancellationToken = default,
            TimeSpan? timeout = default)
        {
            ArgumentNotNull(aggregate, nameof(aggregate), AggregateRootExtensionsCoordinateAsyncAggregateRequired);

            await aggregate
                .GetType()
                .CoordinateAsync(
                    aggregate.Id,
                    operation,
                    cancellationToken: cancellationToken,
                    timeout: timeout);
        }
    }
}