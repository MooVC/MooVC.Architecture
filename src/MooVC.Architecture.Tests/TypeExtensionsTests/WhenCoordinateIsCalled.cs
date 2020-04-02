namespace MooVC.Architecture.TypeExtensionsTests
{
    using System;

    public sealed class WhenCoordinateIsCalled
        : WhenCoordinateIsCalledBase
    {
        protected override void Coordinate(Action operation, TimeSpan? timeout = null)
        {
            GetType().Coordinate(operation, timeout: timeout);
        }
    }
}