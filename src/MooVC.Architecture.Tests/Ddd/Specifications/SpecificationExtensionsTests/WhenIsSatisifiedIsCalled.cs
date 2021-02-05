namespace MooVC.Architecture.Ddd.Specifications.SpecificationExtensionsTests
{
    using Xunit;

    public sealed class WhenIsSatisifiedIsCalled
    {
        [Fact]
        public void GivenAnAllSpecificationTheOutcomeIsPositive()
        {
            bool outcome = TestableSpecification.All.IsSatisfiedBy(1);

            Assert.True(outcome);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenASpecificationTheTheExpectedOutcomeIsReturned(bool expected)
        {
            var specification = new TestableSpecification(expected);
            bool outcome = specification.IsSatisfiedBy(-2);

            Assert.Equal(expected, outcome);
        }
    }
}