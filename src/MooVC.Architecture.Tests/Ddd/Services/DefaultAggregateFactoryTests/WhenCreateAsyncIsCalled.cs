namespace MooVC.Architecture.Ddd.Services.DefaultAggregateFactoryTests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenCreateAsyncIsCalled
{
    [Fact]
    public async Task GivenEventsThenAnAggregateHydratedWithThoseEventsIsReturnedAsync()
    {
        var expected = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();

        expected.Set(new SetRequest(context, Guid.NewGuid()));
        expected.Set(new SetRequest(context, Guid.NewGuid()));

        IEnumerable<DomainEvent> events = expected.GetUncommittedChanges();

        expected.MarkChangesAsCommitted();

        var reconciler = new TestableDefaultAggregateFactory();
        EventCentricAggregateRoot actual = await reconciler.CreateAsync(events);

        Assert.NotSame(expected, actual);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GivenNoDomainEventsThenAnExceptionIsThrownAsync()
    {
        var reconciler = new TestableDefaultAggregateFactory();

        _ = await Assert.ThrowsAsync<DomainEventsMissingException>(
            () => reconciler.CreateAsync(Enumerable.Empty<DomainEvent>()));
    }

    [Fact]
    public async Task GivenNullDomainEventsThenAnExceptionIsThrownAsync()
    {
        var reconciler = new TestableDefaultAggregateFactory();

        _ = await Assert.ThrowsAsync<DomainEventsMissingException>(
            () => reconciler.CreateAsync(default(IEnumerable<DomainEvent>)!));
    }
}