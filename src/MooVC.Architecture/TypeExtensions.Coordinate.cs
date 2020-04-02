namespace MooVC.Architecture
{
    using System;
    using MooVC.Threading;
    using static MooVC.Ensure;
    using static Resources;

    public static partial class TypeExtensions
    {
        public static void Coordinate(this Type type, Action operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(type, nameof(type), TypeExtensionsCoordinateTypeRequired);

            Coordinator.Apply(type.FullName, operation, timeout: timeout);
        }

        public static void Coordinate(this Type type, Guid id, Action operation, TimeSpan? timeout = default)
        {
            ArgumentNotNull(type, nameof(type), TypeExtensionsCoordinateTypeRequired);

            string context = $"{type.FullName}-{id:N}";

            Coordinator.Apply(context, operation, timeout: timeout);
        }
    }
}