namespace MooVC.Architecture.EnsureTests
{
    using System;
    using MooVC.Architecture.Ddd;
    using Xunit;

    public sealed class WhenReferenceIsNotVersionSpecificIsCalled
    {
        [Fact]
        public void GivenAVersionSpecificReferenceThenAnArgumentExceptionIsThrown()
        {
            var reference = new Reference<AggregateRoot>(Guid.NewGuid(), 1);

            ArgumentException exception = Assert.Throws<ArgumentException>(() => Ensure.ReferenceIsNotVersionSpecific(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAVersionSpecificReferenceThenNoExceptionIsThrown()
        {
            var reference = new Reference<AggregateRoot>(Guid.NewGuid());

            Ensure.ReferenceIsNotVersionSpecific(reference, nameof(reference));
        }
    }
}