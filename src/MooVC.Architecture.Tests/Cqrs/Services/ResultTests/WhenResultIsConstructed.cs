namespace MooVC.Architecture.Cqrs.Services.ResultTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenResultIsConstructed
    {
        [Fact]
        public void GivenANullContextThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new SerializableResult<int>(default!, default));
        }

        [Fact]
        public void GivenContextAndAValueThenTheContextAndValuePropertiesAreSetToMatch()
        {
            const int ExpectedValue = 5;

            var context = new SerializableMessage();
            var result = new SerializableResult<int>(context, ExpectedValue);

            Assert.Equal(context.Id, result.CausationId);
            Assert.Equal(context.CorrelationId, result.CorrelationId);
            Assert.Equal(ExpectedValue, result.Value);
        }
    }
}