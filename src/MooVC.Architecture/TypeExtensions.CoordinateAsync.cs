namespace MooVC.Architecture
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Threading;

    public static partial class TypeExtensions
    {
        public static Task CoordinateAsync(this Type type, Func<Task> operation, TimeSpan? timeout = default)
        {
            return Coordinator.ApplyAsync(type.GenerateContext(), operation, timeout: timeout);
        }

        public static Task CoordinateAsync(this Type type, Guid id, Func<Task> operation, TimeSpan? timeout = default)
        {
            string context = $"{type.GenerateContext()}-{id:N}";

            return Coordinator.ApplyAsync(context, operation, timeout: timeout);
        }
    }
}