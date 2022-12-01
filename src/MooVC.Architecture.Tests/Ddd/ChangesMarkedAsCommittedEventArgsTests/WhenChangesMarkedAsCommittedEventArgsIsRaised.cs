namespace MooVC.Architecture.Ddd.ChangesMarkedAsCommittedEventArgsTests;

using System.Collections.Generic;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenChangesMarkedAsCommittedEventArgsIsRaised
{
    [Fact]
    public void GivenChangesThenTheChangesArePropagatedToTheEvent()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();
        ChangesMarkedAsCommittedEventArgs? @event = default;

        aggregate.ChangesMarkedAsCommitted += (sender, e) => @event = e as ChangesMarkedAsCommittedEventArgs;

        IEnumerable<DomainEvent> changes = aggregate.ApplyChanges(context, times: 1);

        Assert.NotNull(@event);
        Assert.Equal(changes, @event.Changes);
    }
}