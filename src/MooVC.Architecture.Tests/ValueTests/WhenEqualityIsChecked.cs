namespace MooVC.Architecture.ValueTests;

using Xunit;

public sealed class WhenEqualityIsChecked
{
    [Fact]
    public void GivenTwoEmptyInstancesThenEqualityIsPositive()
    {
        var first = new EmptyValue();
        var second = new EmptyValue();

        Assert.NotSame(first, second);
        Assert.Equal(first, second);
        Assert.True(first == second);
        Assert.True(first.Equals(second));
        Assert.True(second == first);
    }

    [Theory]
    [InlineData(1, 2, 3, 4, 5, 6)]
    [InlineData(1, 2, 3, 1, 2, 4)]
    [InlineData(-1, -2, -3, -4, -5, -6)]
    [InlineData(1, 0, 0, 0, 0, 1)]
    [InlineData(0, 0, 1, 1, 0, 0)]
    [InlineData(1, 0, 1, 0, 1, 0)]
    [InlineData(-1, 0, 0, 0, 0, -1)]
    [InlineData(0, 0, -1, -1, 0, 0)]
    [InlineData(-1, 0, -1, 0, -1, 0)]
    [InlineData(-1, 0, 1, 1, 0, -1)]
    [InlineData(1, 0, 1, -1, 0, -1)]
    public void GivenTwoInstancesWithDifferentValuesThenEqualityIsNegative(
        int firstA,
        int firstB,
        int firstC,
        int secondA,
        int secondB,
        int secondC)
    {
        var value1 = new TestValue(firstA, firstB, firstC);
        var value2 = new TestValue(secondA, secondB, secondC);

        Assert.NotEqual(value1, value2);
        Assert.False(value1 == value2);
        Assert.False(value1.Equals(value2));
        Assert.False(value2 == value1);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 2, 3)]
    public void GivenTwoInstancesWithEqualValuesThenEqualityIsPositive(
        int valueA,
        int valueB,
        int valueC)
    {
        var value1 = new TestValue(valueA, valueB, valueC);
        var value2 = new TestValue(valueA, valueB, valueC);

        Assert.NotSame(value1, value2);
        Assert.Equal(value1, value2);
        Assert.True(value1 == value2);
        Assert.True(value1.Equals(value2));
        Assert.True(value2 == value1);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(-1, -2, -3)]
    public void GivenTwoInstancesWithTheSameValuesButInADifferentOrderThenEqualityIsNegative(
        int valueA,
        int valueB,
        int valueC)
    {
        var value1 = new TestValue(valueA, valueB, valueC);
        var value2 = new TestValue(valueC, valueB, valueA);

        Assert.NotEqual(value1, value2);
        Assert.False(value1 == value2);
        Assert.False(value1.Equals(value2));
        Assert.False(value2 == value1);
    }
}