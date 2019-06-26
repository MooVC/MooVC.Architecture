namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests
{
    using System;
    using System.Collections.Generic;
    using Xunit;
    using CqrsAggregateRoot = Cqrs.AggregateRoot;

    public sealed class WhenToReferenceIsCalled
    {
        public static IEnumerable<object[]> GivenAReferenceThatMatchesTheTypeSpecifiedThenAStronglyTypedReferenceIsReturnedData => new[]
        {
            new object[] { null },
            new object[] { 3ul }
        };

        [Theory]
        [MemberData(nameof(GivenAReferenceThatMatchesTheTypeSpecifiedThenAStronglyTypedReferenceIsReturnedData))]
        public void GivenAReferenceThatMatchesTheTypeSpecifiedThenAStronglyTypedReferenceIsReturned(ulong? version)
        {
            var aggregateId = Guid.NewGuid();
            IReference reference = new Reference<AggregateRoot>(aggregateId, version: version);

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