namespace MooVC.Architecture.Ddd.GuidExtensionsTests
{
    using System;
    using Xunit;

    public sealed class WhenToReferenceIsCalled
    {
        [Fact]
        public void GivenAnEmptyGuidThenTheEmptyReferenceIsReturned()
        {
            var reference = Guid.Empty.ToReference<AggregateRoot>();

            Assert.Same(Reference<AggregateRoot>.Empty, reference);
        }

        [Fact]
        public void GivenANonEmptyGuidThenAReferenceWithThatIdIsReturned()
        {
            var id = Guid.NewGuid();
            var reference = id.ToReference<AggregateRoot>();

            Assert.NotEqual(Reference<AggregateRoot>.Empty, reference);
            Assert.Equal(id, reference.Id);
        }
    }
}