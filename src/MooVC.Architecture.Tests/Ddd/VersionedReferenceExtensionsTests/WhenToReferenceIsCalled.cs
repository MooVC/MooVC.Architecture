namespace MooVC.Architecture.Ddd.VersionedReferenceExtensionsTests
{
    using System;
    using Xunit;

    public sealed class WhenToReferenceIsCalled
    {
        [Fact]
        public void GivenAMatchingVersionedReferenceThenNoExceptionIsThrown()
        {
            VersionedReference generic = new VersionedReference<AggregateRoot>(Guid.NewGuid());
            VersionedReference<AggregateRoot> typed = generic.ToReference<AggregateRoot>();

            Assert.Same(generic, typed);
        }

        [Fact]
        public void GivenAMatchingEmptyVersionedReferenceThenNoExceptionIsThrown()
        {
            VersionedReference generic = VersionedReference<AggregateRoot>.Empty;
            VersionedReference<AggregateRoot> typed = generic.ToReference<AggregateRoot>();

            Assert.Same(generic, typed);
        }

        [Fact]
        public void GivenAMismatchingVersionedReferenceThenAnArgumentExceptionIsThrown()
        {
            VersionedReference reference = new VersionedReference<EventCentricAggregateRoot>(Guid.NewGuid());

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.ToReference<AggregateRoot>());

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingEmptyVersionedReferenceThenAnArgumentExceptionIsThrown()
        {
            VersionedReference reference = VersionedReference<EventCentricAggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.ToReference<AggregateRoot>());

            Assert.Equal(nameof(reference), exception.ParamName);
        }
    }
}