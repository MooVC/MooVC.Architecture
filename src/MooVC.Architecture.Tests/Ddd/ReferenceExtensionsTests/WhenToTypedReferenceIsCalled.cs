namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests
{
    using System;
    using Xunit;

    public sealed class WhenToTypedReferenceIsCalled
    {
        [Fact]
        public void GivenAMatchingReferenceThenNoExceptionIsThrown()
        {
            Reference generic = new Reference<AggregateRoot>(Guid.NewGuid());
            Reference<AggregateRoot> typed = generic.ToTypedReference<AggregateRoot>();

            Assert.Same(generic, typed);
        }

        [Fact]
        public void GivenAMatchingEmptyReferenceThenNoExceptionIsThrown()
        {
            Reference generic = Reference<AggregateRoot>.Empty;
            Reference<AggregateRoot> typed = generic.ToTypedReference<AggregateRoot>();

            Assert.Same(generic, typed);
        }

        [Fact]
        public void GivenAMismatchingReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference reference = new Reference<EventCentricAggregateRoot>(Guid.NewGuid());

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.ToTypedReference<AggregateRoot>());

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingEmptyReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference reference = Reference<EventCentricAggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.ToTypedReference<AggregateRoot>());

            Assert.Equal(nameof(reference), exception.ParamName);
        }
    }
}