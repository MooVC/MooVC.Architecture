namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using Base = MooVC.Architecture.WhenCoordinateAsyncIsCalled;

    public sealed class WhenCoordinateIsCalled
        : Base
    {
        private readonly Reference reference;

        public WhenCoordinateIsCalled()
        {
            reference = new Reference<SerializableAggregateRoot>(Guid.NewGuid());
        }

        protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
        {
            return reference.CoordinateAsync(operation, timeout: timeout);
        }
    }
}