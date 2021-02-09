namespace MooVC.Architecture.MessageExtensionsTests
{
    using System;
    using System.Threading.Tasks;
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

        protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
        {
            return message.CoordinateAsync(operation, timeout: timeout);
        }
    }
}
