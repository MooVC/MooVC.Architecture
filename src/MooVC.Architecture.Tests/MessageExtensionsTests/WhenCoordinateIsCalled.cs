namespace MooVC.Architecture.MessageExtensionsTests
{
    using System;
    using MooVC.Architecture.MessageTests;

    public sealed class WhenCoordinateIsCalled
        : WhenCoordinateIsCalledBase
    {
        private readonly SerializableMessage message;

        public WhenCoordinateIsCalled()
        {
            message = new SerializableMessage();
        }

        protected override void Coordinate(Action operation, TimeSpan? timeout = default)
        {
            message.Coordinate(operation, timeout: timeout);
        }
    }
}
