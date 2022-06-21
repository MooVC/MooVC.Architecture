namespace MooVC.Architecture.Ddd.ChangesMarkedAsCommittedEventArgsTests;

using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenChangesMarkedAsCommittedEventArgsIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();
        var context = new SerializableMessage();
        ChangesMarkedAsCommittedEventArgs? original = default;

        aggregate.ChangesMarkedAsCommitted += (sender, e) => original = e as ChangesMarkedAsCommittedEventArgs;

        _ = aggregate.ApplyChanges(context, times: 1);

        ChangesMarkedAsCommittedEventArgs? deserialized = original!.Clone();

        Assert.NotSame(original, deserialized);
        Assert.Equal(original!.Changes, deserialized.Changes);
    }
}