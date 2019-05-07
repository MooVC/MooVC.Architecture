namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests
{
    using System;
    using Xunit;
    using CqrsAggregateRoot = MooVC.Architecture.Cqrs.AggregateRoot;

    public sealed class WhenToReferenceIsCalled
    {
        [Fact]
        public void GivenAReferenceThatMatchesTheTypeSpecifiedThenAStronglyTypedReferenceIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            IReference reference = new Reference<AggregateRoot>(aggregateId);

            var value = reference.ToReference<AggregateRoot>();

            Assert.Equal(reference, value);
            Assert.NotSame(reference, value);
        }

        [Fact]
        public void GivenAReferenceThatDoesNotMatchTheTypeSpecifiedThenAnAggregateReferenceMismatchExceptionIsThrown()
        {
            var aggregateId = Guid.NewGuid();
            IReference reference = new Reference<CqrsAggregateRoot>(aggregateId);

            AggregateReferenceMismatchException<AggregateRoot> exception = Assert.Throws<AggregateReferenceMismatchException<AggregateRoot>>(
                () => reference.ToReference<AggregateRoot>());

            Assert.Equal(reference, exception.Reference);
        }
    }
}