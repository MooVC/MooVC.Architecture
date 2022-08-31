namespace MooVC.Architecture.Ddd.Services.SynchronousBusTests;

using System;
using System.Threading.Tasks;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenPublishAsyncIsCalled
{
    [Fact]
    public async Task GivenEventsThenTheEventsArePropagatedAsync()
    {
        bool wasInvoked = false;

        DomainEvent[] expected = new[]
        {
            new SerializableCreatedDomainEvent(new SerializableEventCentricAggregateRoot(), new SerializableMessage()),
        };

        var handler = new TestableSynchronousBus(publish: actual =>
        {
            wasInvoked = true;

            Assert.Equal(expected, actual);
        });

        await handler.PublishAsync(expected);

        Assert.True(wasInvoked);
    }

    [Fact]
    public async Task GivenEventsWhenAnExceptionOccursThenTheExceptionIsThrownAsync()
    {
        var handler = new TestableSynchronousBus();

        DomainEvent[] expected = new[]
        {
            new SerializableCreatedDomainEvent(new SerializableEventCentricAggregateRoot(), new SerializableMessage()),
        };

        _ = await Assert.ThrowsAsync<NotImplementedException>(
            () => handler.PublishAsync(expected));
    }
}