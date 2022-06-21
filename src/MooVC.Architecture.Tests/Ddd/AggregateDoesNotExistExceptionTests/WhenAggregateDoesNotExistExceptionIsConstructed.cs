namespace MooVC.Architecture.Ddd.AggregateDoesNotExistExceptionTests;

using System;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenAggregateDoesNotExistExceptionIsConstructed
{
    [Fact]
    public void GivenAContextTheAnInstanceIsReturnedWithTheContextSet()
    {
        var context = new SerializableMessage();
        var instance = new AggregateDoesNotExistException<AggregateRoot>(context);

        Assert.Equal(context, instance.Context);
    }

    [Fact]
    public void GivenANullContextTheAnArgumentNullExceptionIsThrown()
    {
        Message? context = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new AggregateDoesNotExistException<AggregateRoot>(context!));

        Assert.Equal(nameof(context), exception.ParamName);
    }
}