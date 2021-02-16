namespace MooVC.Architecture
{
    using System;
    using MooVC.Threading;

    public static partial class TypeExtensions
    {
        public static void Coordinate(this Type type, Action operation, TimeSpan? timeout = default)
        {
            Coordinator.Apply(type.GenerateContext(), operation, timeout: timeout);
        }

        public static void Coordinate(this Type type, Guid id, Action operation, TimeSpan? timeout = default)
        {
            string context = $"{type.GenerateContext()}-{id:N}";

            Coordinator.Apply(context, operation, timeout: timeout);
        }
    }
}