namespace MooVC.Architecture.Ddd.EnsureTests
{
    using System;
    using MooVC.Architecture.Ddd;
    using Xunit;

    public sealed class WhenReferenceIsNotVersionSpecificIsCalled
    {
        [Fact]
        public void GivenAnEmptyReferenceWhenAllowEmptyIsFalseThenAnArgumentExceptionIsThrown()
        {
            Reference<AggregateRoot> reference = Reference<AggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(() => Ensure.ReferenceIsNotVersionSpecific(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAnEmptyReferenceAndAMessageWhenAllowEmptyIsFalseThenAnArgumentExceptionIsThrownWithTheMessage()
        {
            Reference<AggregateRoot> reference = Reference<AggregateRoot>.Empty;
            string message = "Some Message";

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => Ensure.ReferenceIsNotVersionSpecific(reference, nameof(reference), message));

            Assert.StartsWith(message, exception.Message);
        }

        [Fact]
        public void GivenAnEmptyReferenceWhenAllowEmptyIsTrueThenNoExceptionIsThrown()
        {
            Reference<AggregateRoot> reference = Reference<AggregateRoot>.Empty;

            Ensure.ReferenceIsNotVersionSpecific(reference, nameof(reference), allowEmpty: true);
        }

        [Fact]
        public void GivenAVersionSpecificReferenceThenAnArgumentExceptionIsThrown()
        {
            var reference = new Reference<AggregateRoot>(Guid.NewGuid(), 1);

            ArgumentException exception = Assert.Throws<ArgumentException>(() => Ensure.ReferenceIsNotVersionSpecific(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAVersionSpecificReferenceAndAMessageThenAnArgumentExceptionIsThrownWithTheMessage()
        {
            var reference = new Reference<AggregateRoot>(Guid.NewGuid(), 1);
            string message = "Some Message";

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => Ensure.ReferenceIsNotVersionSpecific(reference, nameof(reference), message));

            Assert.StartsWith(message, exception.Message);
        }

        [Fact]
        public void GivenAVersionSpecificReferenceThenNoExceptionIsThrown()
        {
            var reference = new Reference<AggregateRoot>(Guid.NewGuid());

            Ensure.ReferenceIsNotVersionSpecific(reference, nameof(reference));
        }
    }
}