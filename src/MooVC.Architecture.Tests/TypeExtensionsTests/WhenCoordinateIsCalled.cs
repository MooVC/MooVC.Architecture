namespace MooVC.Architecture.TypeExtensionsTests
{
    using System;
    using System.Threading.Tasks;

    public sealed class WhenCoordinateIsCalled
        : WhenCoordinateIsCalledBase
    {
        protected override void Coordinate(Action operation, TimeSpan? timeout = default)
        {
            GetType().Coordinate(operation, timeout: timeout);
        }

        protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
        {
            return GetType().CoordinateAsync(operation, timeout: timeout);
        }
    }
}