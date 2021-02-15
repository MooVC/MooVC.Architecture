namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;

    public sealed class WhenCoordinateIsCalled
        : WhenCoordinateIsCalledBase
    {
        private readonly Reference reference;

        public WhenCoordinateIsCalled()
        {
            reference = new Reference<SerializableAggregateRoot>(Guid.NewGuid());
        }

        protected override void Coordinate(Action operation, TimeSpan? timeout = default)
        {
            reference.Coordinate(operation, timeout: timeout);
        }

        protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
        {
            return reference.CoordinateAsync(operation, timeout: timeout);
        }
    }
}