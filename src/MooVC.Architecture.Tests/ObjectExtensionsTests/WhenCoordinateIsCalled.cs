namespace MooVC.Architecture.ObjectExtensionsTests
{
    using System;
    using System.Threading.Tasks;

    public sealed class WhenCoordinateIsCalled
        : WhenCoordinateIsCalledBase
    {
        protected override void Coordinate(Action operation, TimeSpan? timeout = default)
        {
            object @object = new object();

            @object.Coordinate(operation, timeout: timeout);
        }

        protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
        {
            object @object = new object();

            return @object.CoordinateAsync(operation, timeout: timeout);
        }
    }
}