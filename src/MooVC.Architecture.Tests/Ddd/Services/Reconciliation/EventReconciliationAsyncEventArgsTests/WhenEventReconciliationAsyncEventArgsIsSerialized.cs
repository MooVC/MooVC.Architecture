namespace MooVC.Architecture.Ddd.Services.Reconciliation.EventReconciliationAsyncEventArgsTests;

using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenEventReconciliationAsyncEventArgsIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();
        SerializableCreatedDomainEvent[] events = new[] { new SerializableCreatedDomainEvent(aggregate, context) };
        var original = new EventReconciliationAsyncEventArgs(events);
        EventReconciliationAsyncEventArgs deserialized = original.Clone();

        Assert.NotSame(original, deserialized);
        Assert.Equal(original.Events, deserialized.Events);
    }
}