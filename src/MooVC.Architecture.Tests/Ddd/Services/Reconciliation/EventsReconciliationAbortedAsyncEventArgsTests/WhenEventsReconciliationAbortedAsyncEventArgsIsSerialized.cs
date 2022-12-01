namespace MooVC.Architecture.Ddd.Services.Reconciliation.EventsReconciliationAbortedAsyncEventArgsTests;

using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenEventsReconciliationAbortedAsyncEventArgsIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();
        SerializableCreatedDomainEvent[] events = new[] { new SerializableCreatedDomainEvent(aggregate, context) };
        var reason = new InvalidOperationException();
        var original = new EventsReconciliationAbortedAsyncEventArgs(events, reason);
        EventsReconciliationAbortedAsyncEventArgs deserialized = original.Clone();

        Assert.NotSame(original, deserialized);
        Assert.Equal(original.Events, deserialized.Events);
    }
}