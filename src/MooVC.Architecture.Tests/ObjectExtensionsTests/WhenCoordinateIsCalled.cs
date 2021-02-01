namespace MooVC.Architecture.ObjectExtensionsTests
{
    using System;

    public sealed class WhenCoordinateIsCalled
        : WhenCoordinateIsCalledBase
    {
        protected override void Coordinate(Action operation, TimeSpan? timeout = default)
        {
            object @object = new object();

            @object.Coordinate(operation, timeout: timeout);
        }
    }
}