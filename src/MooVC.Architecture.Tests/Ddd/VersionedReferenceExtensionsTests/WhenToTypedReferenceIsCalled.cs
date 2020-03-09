namespace MooVC.Architecture.Ddd.VersionedReferenceExtensionsTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using Xunit;

    public sealed class WhenToTypedReferenceIsCalled
    {
        [Fact]
        public void GivenAMatchingVersionedReferenceThenNoExceptionIsThrown()
        {
            var aggregate = new SerializableAggregateRoot();
            VersionedReference generic = new VersionedReference<AggregateRoot>(aggregate);
            VersionedReference<AggregateRoot> typed = generic.ToTypedReference<AggregateRoot>();

            Assert.Same(generic, typed);
        }

        [Fact]
        public void GivenAMatchingEmptyVersionedReferenceThenNoExceptionIsThrown()
        {
            VersionedReference generic = VersionedReference<AggregateRoot>.Empty;
            VersionedReference<AggregateRoot> typed = generic.ToTypedReference<AggregateRoot>();

            Assert.Same(generic, typed);
        }

        [Fact]
        public void GivenAMismatchingVersionedReferenceThenAnArgumentExceptionIsThrown()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            VersionedReference reference = new VersionedReference<EventCentricAggregateRoot>(aggregate);

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.ToTypedReference<AggregateRoot>());

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingEmptyVersionedReferenceThenAnArgumentExceptionIsThrown()
        {
            VersionedReference reference = VersionedReference<EventCentricAggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.ToTypedReference<AggregateRoot>());

            Assert.Equal(nameof(reference), exception.ParamName);
        }
    }
}