namespace MooVC.Architecture.Cqrs.Services.SynchronousQueryHandlerTests
{
    using System;
    using System.Threading.Tasks;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenExecuteAsyncIsCalled
    {
        [Fact]
        public async Task GivenNoQueryThenAResultIsReturnedAsync()
        {
            bool wasInvoked = false;
            var expected = new SerializableMessage();

            var handler = new TestableSynchronousQueryHandler<SerializableMessage>(execute: () =>
            {
                wasInvoked = true;

                return expected;
            });

            SerializableMessage? actual = await handler.ExecuteAsync();

            Assert.True(wasInvoked);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async Task GivenAQueryThenAResultIsReturnedAsync()
        {
            bool wasInvoked = false;
            var expectedQuery = new SerializableMessage();
            var expectedResult = new SerializableMessage();

            var handler = new TestableSynchronousQueryHandler<Message, SerializableMessage>(execute: actualQuery =>
            {
                wasInvoked = true;

                Assert.Equal(expectedQuery, actualQuery);

                return expectedResult;
            });

            SerializableMessage? actualResult = await handler.ExecuteAsync(expectedQuery);

            Assert.True(wasInvoked);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GivenNoQueryWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
        {
            var handler = new TestableSynchronousQueryHandler<SerializableMessage>();

            _ = await Assert.ThrowsAsync<NotImplementedException>(
                () => handler.ExecuteAsync());
        }

        [Fact]
        public async Task GivenAQueryWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
        {
            var handler = new TestableSynchronousQueryHandler<Message, SerializableMessage>();

            _ = await Assert.ThrowsAsync<NotImplementedException>(
                () => handler.ExecuteAsync(new SerializableMessage()));
        }
    }
}