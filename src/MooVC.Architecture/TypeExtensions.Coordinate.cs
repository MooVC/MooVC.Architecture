namespace MooVC.Architecture
{
    using System;
    using MooVC.Threading;

    internal static partial class TypeExtensions
    {
        public static void Coordinate(this Type type, Guid id, Action operation, TimeSpan? timeout = default)
        {
            string context = $"{type.FullName}-{id:N}";

            Coordinator.Apply(context, operation, timeout: timeout);
        }
    }
}