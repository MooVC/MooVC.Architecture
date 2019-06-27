namespace MooVC.Architecture.EnsureTests
{
    using System;
    using MooVC.Architecture.Ddd;
    using Xunit;

    public sealed class WhenReferenceIsNotEmptyIsCalled
    {
        [Fact]
        public void GivenAnEmptyReferenceThenAnArgumentExceptionIsThrown()
        {
            Reference<AggregateRoot> reference = Reference<AggregateRoot>.Empty;

            ArgumentException exception = Assert.Throws<ArgumentException>(() => Ensure.ReferenceIsNotEmpty(reference, nameof(reference)));

            Assert.Equal(nameof(reference), exception.ParamName);
        }

        [Fact]
        public void GivenANonEmptyReferenceThenNoExceptionIsThrown()
        {
            var reference = new Reference<AggregateRoot>(Guid.NewGuid());

            Ensure.ReferenceIsNotEmpty(reference, nameof(reference));
        }
    }
}