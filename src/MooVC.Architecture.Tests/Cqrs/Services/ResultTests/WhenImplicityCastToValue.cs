namespace MooVC.Architecture.Cqrs.Services.ResultTests
{
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenImplicityCastToValue
    {
        [Fact]
        public void GivenAResultThenTheValueIsReturned()
        {
            const int Expected = 9;

            var context = new SerializableMessage();
            var result = new SerializableResult<int>(context, Expected);
            int actual = result;

            Assert.Equal(Expected, actual);
        }
    }
}