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
            Reference reference = Reference<SerializableAggregateRoot>.Create(Guid.NewGuid());

            ReferenceIsOfType<SerializableAggregateRoot>(reference, nameof(reference));
        }

        [Fact]
        public void GivenAMatchingEmptyReferenceThenNoExceptionIsThrown()
        {
            Reference reference = Reference<SerializableAggregateRoot>.Empty;

            ReferenceIsOfType<SerializableAggregateRoot>(reference, nameof(reference));
        }

        [Fact]
        public void GivenAMismatchingReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference reference = Reference<SerializableEventCentricAggregateRoot>.Create(Guid.NewGuid());

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<SerializableAggregateRoot>(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingEmptyReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference reference = Reference<SerializableEventCentricAggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<SerializableAggregateRoot>(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAMismatchingReferenceAndAMessageThenAnArgumentExceptionIsThrownWithTheMessageProvided()
        {
            Reference reference = Reference<SerializableEventCentricAggregateRoot>.Create(Guid.NewGuid());
            string message = "Some sessage";

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<SerializableAggregateRoot>(reference, nameof(reference), message));

            Assert.StartsWith(message, exception.Message);
        }

        [Fact]
        public void GivenAMismatchingEmptyReferenceAndAMessageThenAnArgumentExceptionIsThrownWithTheMessageProvided()
        {
            Reference reference = Reference<SerializableEventCentricAggregateRoot>.Empty;
            string message = "Some sessage";

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => ReferenceIsOfType<SerializableAggregateRoot>(reference, nameof(reference), message));

            Assert.StartsWith(message, exception.Message);
        }
    }
}