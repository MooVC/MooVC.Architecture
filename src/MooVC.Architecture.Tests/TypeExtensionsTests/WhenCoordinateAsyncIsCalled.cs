namespace MooVC.Architecture.TypeExtensionsTests;

using System;
using System.Threading.Tasks;
using Base = MooVC.Architecture.WhenCoordinateAsyncIsCalled;

public sealed class WhenCoordinateAsyncIsCalled
    : Base
{
    protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
    {
        return GetType().CoordinateAsync(operation, timeout: timeout);
    }
}