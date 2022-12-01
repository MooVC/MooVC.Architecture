namespace MooVC.Architecture.Ddd.Services.AggregateVersionNotFoundExceptionTests;

using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenAggregateVersionNotFoundExceptionIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var subject = new SerializableAggregateRoot();
        var aggregate = subject.ToReference();
        var context = new SerializableMessage();
        var original = new AggregateVersionNotFoundException<SerializableAggregateRoot>(aggregate, context);
        AggregateVersionNotFoundException<SerializableAggregateRoot> deserialized = original.Clone();

        Assert.NotSame(original, deserialized);
        Assert.Equal(original.Aggregate, deserialized.Aggregate);
        Assert.Equal(original.Context, deserialized.Context);
    }
}