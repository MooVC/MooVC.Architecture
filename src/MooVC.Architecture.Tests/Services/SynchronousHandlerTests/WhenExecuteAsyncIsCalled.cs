namespace MooVC.Architecture.Services.SynchronousHandlerTests;

using System;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenExecuteAsyncIsCalled
{
    [Fact]
    public async Task GivenAMessageThenTheMessageIsPropagatedAsync()
    {
        bool wasInvoked = false;
        var expected = new SerializableMessage();

        var handler = new TestableSynchronousHandler<Message>(execute: actual =>
        {
            wasInvoked = true;

            Assert.Equal(expected, actual);
        });

        await handler.ExecuteAsync(expected, CancellationToken.None);

        Assert.True(wasInvoked);
    }

    [Fact]
    public async Task GivenAMessageWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
    {
        var handler = new TestableSynchronousHandler<Message>();

        _ = await Assert.ThrowsAsync<NotImplementedException>(
            () => handler.ExecuteAsync(new SerializableMessage(), CancellationToken.None));
    }
}