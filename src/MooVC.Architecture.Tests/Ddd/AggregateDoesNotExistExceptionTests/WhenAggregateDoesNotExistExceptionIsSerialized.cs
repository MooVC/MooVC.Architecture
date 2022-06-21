namespace MooVC.Architecture.Ddd.AggregateDoesNotExistExceptionTests;

using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenAggregateDoesNotExistExceptionIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var context = new SerializableMessage();
        var original = new AggregateDoesNotExistException<AggregateRoot>(context);
        AggregateDoesNotExistException<AggregateRoot> deserialized = original.Clone();

        Assert.NotSame(original, deserialized);
        Assert.Equal(original.Context, deserialized.Context);
    }
}