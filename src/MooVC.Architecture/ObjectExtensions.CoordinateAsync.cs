namespace MooVC.Architecture
{
    using System;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Resources;
    using static MooVC.Ensure;

    public static partial class ObjectExtensions
    {
        public static async Task CoordinateAsync(this object target, Func<Task> operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(target, nameof(target), ObjectExtensionsCoordinateObjectRequired);

            await target
                .GetType()
                .CoordinateAsync(operation, timeout: timeout);
        }
    }
}