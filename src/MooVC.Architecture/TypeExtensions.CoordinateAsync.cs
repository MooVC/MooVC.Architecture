namespace MooVC.Architecture;

using System;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Threading;

public static partial class TypeExtensions
{
    public static Task CoordinateAsync(
        this Type type,
        Func<Task> operation,
        CancellationToken? cancellationToken = default,
        TimeSpan? timeout = default)
    {
        return Coordinator.ApplyAsync(
            type.GenerateContext(),
            operation,
            cancellationToken: cancellationToken,
            timeout: timeout);
    }

    public static Task CoordinateAsync(
        this Type type,
        Guid id,
        Func<Task> operation,
        CancellationToken? cancellationToken = default,
        TimeSpan? timeout = default)
    {
        string context = $"{type.GenerateContext()}-{id:N}";

        return Coordinator.ApplyAsync(
            context,
            operation,
            cancellationToken: cancellationToken,
            timeout: timeout);
    }
}