namespace MooVC.Architecture.ObjectExtensionsTests;

using System;
using System.Threading.Tasks;
using Base = MooVC.Architecture.WhenCoordinateAsyncIsCalled;

public sealed class WhenCoordinateIsCalled
    : Base
{
    protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
    {
        object @object = new();

        return @object.CoordinateAsync(operation, timeout: timeout);
    }
}