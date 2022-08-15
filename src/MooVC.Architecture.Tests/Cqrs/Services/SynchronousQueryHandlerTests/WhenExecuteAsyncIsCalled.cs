namespace MooVC.Architecture.Cqrs.Services.SynchronousQueryHandlerTests;

using System;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenExecuteAsyncIsCalled
{
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

        SerializableMessage? actualResult = await handler.ExecuteAsync(expectedQuery, CancellationToken.None);

        Assert.True(wasInvoked);
        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public async Task GivenAQueryWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
    {
        var handler = new TestableSynchronousQueryHandler<Message, SerializableMessage>();

        _ = await Assert.ThrowsAsync<NotImplementedException>(
            () => handler.ExecuteAsync(new SerializableMessage(), CancellationToken.None));
    }
}