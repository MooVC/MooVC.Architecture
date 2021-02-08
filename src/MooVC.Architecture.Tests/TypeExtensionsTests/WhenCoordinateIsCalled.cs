namespace MooVC.Architecture.TypeExtensionsTests
{
    using System;

    public sealed class WhenCoordinateIsCalled
        : WhenCoordinateIsCalledBase
    {
        protected override void Coordinate(Action operation, TimeSpan? timeout = default)
        {
            GetType().Coordinate(operation, timeout: timeout);
        }
    }
}