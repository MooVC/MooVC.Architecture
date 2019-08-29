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
            IReference reference = new Reference<AggregateRoot>(Guid.NewGuid(), AggregateRoot.DefaultVersion);

            ReferenceIsOfType<AggregateRoot>(reference, nameof(reference));
        }

        [Fact]
        public void GivenAMatchingEmptyReferenceThenNoExceptionIsThrown()
        {
            IReference reference = Reference<AggregateRoot>.Empty;

            ReferenceIsOfType<AggregateRoot>(reference, nameof(reference));
        }

        [Fact]
        public void GivenAMismatchingReferenceThenAnArgumentExceptionIsThrown()
        {
            IReference reference = new Reference<EventCentricAggregateRoot>(Guid.NewGuid(), AggregateRoot.DefaultVersion);

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<AggregateRoot>(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingEmptyReferenceThenAnArgumentExceptionIsThrown()
        {
            IReference reference = Reference<EventCentricAggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<AggregateRoot>(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingReferenceAndAMessageThenAnArgumentExceptionIsThrownWithTheMessageProvided()
        {
            IReference reference = new Reference<EventCentricAggregateRoot>(Guid.NewGuid(), AggregateRoot.DefaultVersion);
            string message = "Some sessage";

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<AggregateRoot>(reference, nameof(reference), message));

            Assert.StartsWith(message, exception.Message);
        }

        [Fact]
        public void GivenAMismatchingEmptyReferenceAndAMessageThenAnArgumentExceptionIsThrownWithTheMessageProvided()
        {
            IReference reference = Reference<EventCentricAggregateRoot>.Empty;
            string message = "Some sessage";

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<AggregateRoot>(reference, nameof(reference), message));

            Assert.StartsWith(message, exception.Message);
        }
    }
}