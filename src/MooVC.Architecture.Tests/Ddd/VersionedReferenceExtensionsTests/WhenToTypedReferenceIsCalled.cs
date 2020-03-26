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
            VersionedReference generic = new VersionedReference<SerializableAggregateRoot>(aggregate);
            VersionedReference<SerializableAggregateRoot> typed = generic.ToTypedReference<SerializableAggregateRoot>();

            Assert.Same(generic, typed);
        }

        [Fact]
        public void GivenAMatchingEmptyVersionedReferenceThenNoExceptionIsThrown()
        {
            VersionedReference generic = VersionedReference<SerializableAggregateRoot>.Empty;
            VersionedReference<SerializableAggregateRoot> typed = generic.ToTypedReference<SerializableAggregateRoot>();

            Assert.Same(generic, typed);
        }

        [Fact]
        public void GivenAMismatchingVersionedReferenceThenAnArgumentExceptionIsThrown()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            VersionedReference reference = new VersionedReference<SerializableEventCentricAggregateRoot>(aggregate);

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.ToTypedReference<AggregateRoot>());

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingEmptyVersionedReferenceThenAnArgumentExceptionIsThrown()
        {
            VersionedReference reference = VersionedReference<SerializableEventCentricAggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.ToTypedReference<SerializableAggregateRoot>());

            Assert.Equal(nameof(reference), exception.ParamName);
        }
    }
}