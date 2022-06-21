namespace MooVC.Architecture.Ddd.Services.AggregateConflictDetectedExceptionTests;

using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenAggregateConflictDetectedExceptionIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var subject = new SerializableEventCentricAggregateRoot();
        var aggregate = subject.ToReference();

        var original = new AggregateConflictDetectedException<SerializableEventCentricAggregateRoot>(
            aggregate,
            subject.Version,
            subject.Version);

        AggregateConflictDetectedException<SerializableEventCentricAggregateRoot> deserialized = original.Clone();

        Assert.NotSame(original, deserialized);
        Assert.Equal(original.Aggregate, deserialized.Aggregate);
        Assert.Equal(original.Received, deserialized.Received);
        Assert.Equal(original.Persisted, deserialized.Persisted);
    }
}