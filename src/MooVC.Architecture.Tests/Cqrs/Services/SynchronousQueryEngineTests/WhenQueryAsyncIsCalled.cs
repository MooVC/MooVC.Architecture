namespace MooVC.Architecture.Cqrs.Services.SynchronousQueryEngineTests;

using System;
using System.Threading.Tasks;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenQueryAsyncIsCalled
{
    [Fact]
    public async Task GivenNoQueryThenAResultIsReturnedAsync()
    {
        bool wasInvoked = false;
        var expected = new SerializableMessage();

        var engine = new TestableSynchronousQueryEngine(parameterless: () =>
        {
            wasInvoked = true;

            return expected;
        });

        SerializableMessage? actual = await engine.QueryAsync<SerializableMessage>();

        Assert.True(wasInvoked);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GivenAQueryThenAResultIsReturnedAsync()
    {
        bool wasInvoked = false;
        var expectedQuery = new SerializableMessage();
        var expectedResult = new SerializableMessage();

        var engine = new TestableSynchronousQueryEngine(parameters: actualQuery =>
        {
            wasInvoked = true;

            Assert.Equal(expectedQuery, actualQuery);

            return expectedResult;
        });

        SerializableMessage? actualResult = await engine
            .QueryAsync<SerializableMessage, SerializableMessage>(expectedQuery);

        Assert.True(wasInvoked);
        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public async Task GivenNoQueryWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
    {
        var engine = new TestableSynchronousQueryEngine();

        _ = await Assert.ThrowsAsync<NotImplementedException>(
            () => engine.QueryAsync<Message>());
    }

    [Fact]
    public async Task GivenAQueryWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
    {
        var engine = new TestableSynchronousQueryEngine();

        _ = await Assert.ThrowsAsync<NotImplementedException>(
            () => engine.QueryAsync<SerializableMessage, Message>(new SerializableMessage()));
    }
}