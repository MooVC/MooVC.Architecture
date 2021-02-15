namespace MooVC.Architecture
{
    using System;
    using System.Threading.Tasks;
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

        public static async Task CoordinateAsync(this Type type, Func<Task> operation, TimeSpan? timeout = default)
        {
            await Coordinator.ApplyAsync(type.GenerateContext(), operation, timeout: timeout);
        }

        public static async Task CoordinateAsync(this Type type, Guid id, Func<Task> operation, TimeSpan? timeout = default)
        {
            string context = $"{type.GenerateContext()}-{id:N}";

            await Coordinator.ApplyAsync(context, operation, timeout: timeout);
        }
    }
}