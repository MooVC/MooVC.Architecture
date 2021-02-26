namespace MooVC.Architecture
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Resources;
    using static MooVC.Ensure;

    public static partial class ObjectExtensions
    {
        public static async Task CoordinateAsync(
            this object target,
            Func<Task> operation,
            CancellationToken? cancellationToken = default,
            TimeSpan? timeout = default)
        {
            ArgumentNotNull(target, nameof(target), ObjectExtensionsCoordinateAsyncObjectRequired);

            await target
                .GetType()
                .CoordinateAsync(
                    operation,
                    cancellationToken: cancellationToken,
                    timeout: timeout);
        }
    }
}