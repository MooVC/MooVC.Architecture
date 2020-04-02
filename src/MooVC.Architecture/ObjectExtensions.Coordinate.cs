namespace MooVC.Architecture
{
    using System;
    using static MooVC.Ensure;
    using static Resources;

    public static partial class ObjectExtensions
    {
        public static void Coordinate(this object target, Action operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(target, nameof(target), ObjectExtensionsCoordinateObjectRequired);

            target
                .GetType()
                .Coordinate(operation, timeout: timeout);
        }
    }
}