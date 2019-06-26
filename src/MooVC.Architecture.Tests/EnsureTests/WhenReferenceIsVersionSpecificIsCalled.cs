namespace MooVC.Architecture.EnsureTests
{
    using System;
    using MooVC.Architecture.Ddd;
    using Xunit;

    public sealed class WhenReferenceIsVersionSpecificIsCalled
    {
        [Fact]
        public void GivenAVersionSpecificReferenceThenAnArgumentExceptionIsThrown()
        {
            var reference = new Reference<AggregateRoot>(Guid.NewGuid());

            ArgumentException exception = Assert.Throws<ArgumentException>(() => Ensure.ReferenceIsVersionSpecific(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenAVersionSpecificReferenceThenNoExceptionIsThrown()
        {
            var reference = new Reference<AggregateRoot>(Guid.NewGuid(), 1);

            Ensure.ReferenceIsVersionSpecific(reference, nameof(reference));
        }
    }
}