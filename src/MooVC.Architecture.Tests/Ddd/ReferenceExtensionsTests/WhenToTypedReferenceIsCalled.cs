namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using Xunit;

    public sealed class WhenToTypedReferenceIsCalled
    {
        [Fact]
        public void GivenAMatchingReferenceThenNoExceptionIsThrown()
        {
            Reference generic = new Reference<SerializableAggregateRoot>(Guid.NewGuid());
            Reference<SerializableAggregateRoot> typed = generic.ToTypedReference<SerializableAggregateRoot>();

            Assert.Same(generic, typed);
        }

        [Fact]
        public void GivenAMatchingEmptyReferenceThenNoExceptionIsThrown()
        {
            Reference generic = Reference<SerializableAggregateRoot>.Empty;
            Reference<SerializableAggregateRoot> typed = generic.ToTypedReference<SerializableAggregateRoot>();

            Assert.Same(generic, typed);
        }

        [Fact]
        public void GivenAMismatchingReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference reference = new Reference<SerializableEventCentricAggregateRoot>(Guid.NewGuid());

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.ToTypedReference<SerializableAggregateRoot>());

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingEmptyReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference reference = Reference<SerializableEventCentricAggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.ToTypedReference<SerializableAggregateRoot>());

            Assert.Equal(nameof(reference), exception.ParamName);
        }
    }
}