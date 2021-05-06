namespace MooVC.Architecture.MessageExtensionsTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.MessageTests;
    using Base = MooVC.Architecture.WhenCoordinateAsyncIsCalled;

    public sealed class WhenCoordinateAsyncIsCalled
        : Base
    {
        private readonly SerializableMessage message;

        public WhenCoordinateAsyncIsCalled()
        {
            message = new SerializableMessage();
        }

        protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
        {
            return message.CoordinateAsync(operation, timeout: timeout);
        }
    }
}