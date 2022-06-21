namespace MooVC.Architecture.EntityTests;

using Xunit;

public sealed class WhenEntityIsConstructed
{
    [Fact]
    public void GivenAnIdTheTheIdIsPropagated()
    {
        const int ExpectedId = 1;

        var entity = new SerializableEntity<int>(ExpectedId);

        Assert.Equal(ExpectedId, entity.Id);
    }
}