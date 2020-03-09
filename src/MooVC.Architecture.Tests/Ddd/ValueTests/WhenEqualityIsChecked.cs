namespace MooVC.Architecture.Ddd.ValueTests
{
    using Xunit;

    public sealed class WhenEqualityIsChecked
    {
        [Fact]
        public void GivenTwoInstancesWithDifferentValuesThenEqualityIsNegative()
        {
            var value1 = new TestValue(1, 2);
            var value2 = new TestValue(3, 4);

            Assert.NotEqual(value1, value2);
        }

        [Fact]
        public void GivenTwoInstancesWithEqualValuesThenEqualityIsPositive()
        {
            var value1 = new TestValue(1, 2);
            var value2 = new TestValue(1, 2);

            Assert.Equal(value1, value2);
        }

        [Fact]
        public void GivenTwoInstancesWithTheSameValuesButInADifferentOrderThenEqualityIsNegative()
        {
            var value1 = new TestValue(1, 2);
            var value2 = new TestValue(2, 1);

            Assert.NotEqual(value1, value2);
        }
    }
}
