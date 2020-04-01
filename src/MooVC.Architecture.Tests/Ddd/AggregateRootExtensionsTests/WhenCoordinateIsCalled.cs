namespace MooVC.Architecture.Ddd.AggregateRootExtensionsTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;

    public sealed class WhenCoordinateIsCalled
        : WhenCoordinateIsCalledBase
    {
        private readonly SerializableAggregateRoot aggregate;

        public WhenCoordinateIsCalled()
        {
            aggregate = new SerializableAggregateRoot();
        }

        protected override void Coordinate(Action operation, TimeSpan? timeout = null)
        {
            aggregate.Coordinate(operation, timeout: timeout);
        }
    }
}
