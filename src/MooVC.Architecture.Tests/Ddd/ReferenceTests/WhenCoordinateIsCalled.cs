namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Ddd.Reference;
    using Base = MooVC.Architecture.WhenCoordinateAsyncIsCalled;

    public sealed class WhenCoordinateIsCalled
        : Base
    {
        private readonly Reference reference;

        public WhenCoordinateIsCalled()
        {
            reference = Create<SerializableAggregateRoot>(Guid.NewGuid());
        }

        protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
        {
            return reference.CoordinateAsync(operation, timeout: timeout);
        }
    }
}