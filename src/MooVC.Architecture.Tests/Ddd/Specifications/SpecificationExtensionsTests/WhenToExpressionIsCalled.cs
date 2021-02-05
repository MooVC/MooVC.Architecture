namespace MooVC.Architecture.Ddd.Specifications.SpecificationExtensionsTests
{
    using Xunit;

    public sealed class WhenToExpressionIsCalled
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GivenASpecificationTheExpressionReturnedMatchesTheOutcomeOfIsSatisifiedBy(bool expected)
        {
            var specification = new TestableSpecification(expected);
            bool isSatisified = specification.IsSatisfiedBy(-2);

            bool outcome = specification
                .ToExpression()
                .Compile()
                .Invoke(-2);

            Assert.Equal(expected, outcome);
            Assert.Equal(isSatisified, outcome);
        }
    }
}