namespace MooVC.Architecture.Ddd.Services.Reconciliation.SequencedEventsTests;

using MooVC.Architecture.Ddd.DomainEventTests;
using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenSequencedEventsIsSerialized
{
    [Theory]
    [InlineData(ulong.MinValue)]
    [InlineData(ulong.MaxValue)]
    public void GivenAnInstanceThenAllPropertiesAreSerialized(ulong sequence)
    {
        var aggregate = new SerializableAggregateRoot();
        var context = new SerializableMessage();

        SerializableDomainEvent<SerializableAggregateRoot>[] events = new[]
        {
            new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate),
        };

        var original = new SequencedEvents(sequence, events);
        SequencedEvents deserialized = original.Clone();

        Assert.Equal(original.Sequence, deserialized.Sequence);
        Assert.Equal(original.Events, deserialized.Events);
        Assert.NotSame(original, deserialized);
    }
}