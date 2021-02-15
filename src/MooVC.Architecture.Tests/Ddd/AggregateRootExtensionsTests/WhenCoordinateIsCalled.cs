namespace MooVC.Architecture.Ddd.AggregateRootExtensionsTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;

    public sealed class WhenCoordinateIsCalled
        : WhenCoordinateIsCalledBase
    {
        private readonly SerializableAggregateRoot aggregate;

        public WhenCoordinateIsCalled()
        {
            aggregate = new SerializableAggregateRoot();
        }

        protected override void Coordinate(Action operation, TimeSpan? timeout = default)
        {
            aggregate.Coordinate(operation, timeout: timeout);
        }

        protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
        {
            return aggregate.CoordinateAsync(operation, timeout: timeout);
        }
    }
}
