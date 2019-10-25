namespace MooVC.Architecture.Ddd.EnsureTests
{
    using System;
    using MooVC.Architecture.Ddd;
    using Xunit;
    using static MooVC.Architecture.Ddd.Ensure;

    public sealed class WhenReferenceIsNotEmptyIsCalled
    {
        [Fact]
        public void GivenAnEmptyReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference reference = Reference<AggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(() => ReferenceIsNotEmpty(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAnEmptyReferenceAndAMessageThenAnArgumentExceptionIsThrownWithTheMessageProvided()
        {
            Reference reference = Reference<AggregateRoot>.Empty;
            string message = "Some sessage";

            ArgumentException exception = Assert.Throws<ArgumentException>(() => ReferenceIsNotEmpty(reference, nameof(reference), message));

            Assert.StartsWith(message, exception.Message);
        }

        [Fact]
        public void GivenANonEmptyReferenceThenNoExceptionIsThrown()
        {
            var reference = new Reference<AggregateRoot>(Guid.NewGuid());

            ReferenceIsNotEmpty(reference, nameof(reference));
        }
    }
}