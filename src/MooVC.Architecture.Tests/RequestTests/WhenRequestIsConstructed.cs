namespace MooVC.Architecture.RequestTests;

using System;
using MooVC.Architecture.MessageTests;
using Xunit;

public sealed class WhenRequestIsConstructed
{
    [Fact]
    public void GivenAContextThenTheContextIsPropagated()
    {
        var expectedContext = new SerializableMessage();
        var request = new TestableRequest(expectedContext);

        Assert.Equal(expectedContext, request.Context);
    }

    [Fact]
    public void GivenNoContextThenAnArgumentNullExceptionIsThrown()
    {
        _ = Assert.Throws<ArgumentNullException>(
            () => new TestableRequest(default!));
    }
}