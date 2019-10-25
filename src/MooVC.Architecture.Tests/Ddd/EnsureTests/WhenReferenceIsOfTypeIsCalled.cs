namespace MooVC.Architecture.Ddd.EnsureTests
{
    using System;
    using MooVC.Architecture.Ddd;
    using Xunit;
    using static MooVC.Architecture.Ddd.Ensure;

    public sealed class WhenReferenceIsOfTypeIsCalled
    {
        [Fact]
        public void GivenAMatchingReferenceThenNoExceptionIsThrown()
        {
            Reference reference = new Reference<AggregateRoot>(Guid.NewGuid());

            ReferenceIsOfType<AggregateRoot>(reference, nameof(reference));
        }

        [Fact]
        public void GivenAMatchingEmptyReferenceThenNoExceptionIsThrown()
        {
            Reference reference = Reference<AggregateRoot>.Empty;

            ReferenceIsOfType<AggregateRoot>(reference, nameof(reference));
        }

        [Fact]
        public void GivenAMismatchingReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference reference = new Reference<EventCentricAggregateRoot>(Guid.NewGuid());

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<AggregateRoot>(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingEmptyReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference reference = Reference<EventCentricAggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<AggregateRoot>(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingReferenceAndAMessageThenAnArgumentExceptionIsThrownWithTheMessageProvided()
        {
            Reference reference = new Reference<EventCentricAggregateRoot>(Guid.NewGuid());
            string message = "Some sessage";

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<AggregateRoot>(reference, nameof(reference), message));

            Assert.StartsWith(message, exception.Message);
        }

        [Fact]
        public void GivenAMismatchingEmptyReferenceAndAMessageThenAnArgumentExceptionIsThrownWithTheMessageProvided()
        {
            Reference reference = Reference<EventCentricAggregateRoot>.Empty;
            string message = "Some sessage";

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<AggregateRoot>(reference, nameof(reference), message));

            Assert.StartsWith(message, exception.Message);
        }
    }
}