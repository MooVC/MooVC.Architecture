namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;

    public sealed class WhenCoordinateIsCalled
        : WhenCoordinateIsCalledBase
    {
        private readonly Reference reference;

        public WhenCoordinateIsCalled()
        {
            reference = new Reference<SerializableAggregateRoot>(Guid.NewGuid());
        }

        protected override void Coordinate(Action operation, TimeSpan? timeout = null)
        {
            reference.Coordinate(operation, timeout: timeout);
        }
    }
}