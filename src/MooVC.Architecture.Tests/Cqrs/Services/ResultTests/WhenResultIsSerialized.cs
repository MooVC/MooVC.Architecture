namespace MooVC.Architecture.Cqrs.Services.ResultTests;

using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using Xunit;

public sealed class WhenResultIsSerialized
{
    [Fact]
    public void GivenAnInstanceThenAllPropertiesAreSerialized()
    {
        var context = new SerializableMessage();
        var result = new SerializableResult<int>(context, 1);
        SerializableResult<int> deserialized = result.Clone();

        Assert.Equal(result, deserialized);
        Assert.NotSame(result, deserialized);
        Assert.Equal(result.Value, deserialized.Value);
        Assert.Equal(result.GetHashCode(), deserialized.GetHashCode());
    }
}