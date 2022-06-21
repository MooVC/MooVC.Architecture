namespace MooVC.Architecture.EntityMaximumIdValueExceededExceptionTests;

using System;
using Xunit;

public sealed class WhenEntityMaximumIdValueExceededExceptionIsConstructed
{
    [Fact]
    public void GivenALongMaxAndATypeThenAnInstanceIsReturned()
    {
        long max = -1;
        Type type = GetType();
        _ = new EntityMaximumIdValueExceededException(max, type);
    }

    [Fact]
    public void GivenAULongMaxAndATypeThenAnInstanceIsReturned()
    {
        ulong max = 1;
        Type type = GetType();
        _ = new EntityMaximumIdValueExceededException(max, type);
    }

    [Fact]
    public void GivenALongMaxAndANullTypeThenAnArgumentNullExceptionIsThrown()
    {
        ulong max = 1;
        Type? type = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new EntityMaximumIdValueExceededException(max, type!));

        Assert.Equal(nameof(type), exception.ParamName);
    }

    [Fact]
    public void GivenAULongMaxAndANullTypeThenAnArgumentNullExceptionIsThrown()
    {
        ulong max = 1;
        Type? type = default;

        ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
            () => new EntityMaximumIdValueExceededException(max, type!));

        Assert.Equal(nameof(type), exception.ParamName);
    }
}