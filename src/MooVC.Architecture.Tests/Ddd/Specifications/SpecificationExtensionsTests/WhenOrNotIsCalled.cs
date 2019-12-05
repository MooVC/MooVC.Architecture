namespace MooVC.Architecture.Ddd.Specifications.SpecificationExtensionsTests
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public sealed class WhenOrNotIsCalled
    {
        public static IEnumerable<object[]> GivenANullSpecificationThenAnArgumentNullExceptionIsThrownData()
        {
            var specification = new Mock<Specification<int>>();

            return new[]
            {
                new object[] { null, null },
                new object[] { null, specification.Object },
                new object[] { specification.Object, null },
            };
        }

        public static IEnumerable<object[]> GivenTwoSpecificationsThenTheExpressionResultMatchesTheConditionalOrNotOutcomeForBothValuesData()
        {
            var positive = new Mock<Specification<int>>();
            var negative = new Mock<Specification<int>>();

            _ = positive.Setup(spec => spec.ToExpression()).Returns(spec => true);
            _ = negative.Setup(spec => spec.ToExpression()).Returns(spec => false);

            return new[]
            {
                new object[] { negative.Object, negative.Object, true },
                new object[] { negative.Object, positive.Object, false },
                new object[] { positive.Object, negative.Object, true },
                new object[] { positive.Object, positive.Object, true },
            };
        }

        [Theory]
        [MemberData(nameof(GivenANullSpecificationThenAnArgumentNullExceptionIsThrownData))]
        public void GivenANullSpecificationThenAnArgumentNullExceptionIsThrown(Specification<int> first, Specification<int> second)
        {
            _ = Assert.Throws<ArgumentNullException>(() => first.OrNot(second));
        }

        [Theory]
        [MemberData(nameof(GivenTwoSpecificationsThenTheExpressionResultMatchesTheConditionalOrNotOutcomeForBothValuesData))]
        public void GivenTwoSpecificationsThenTheExpressionResultMatchesTheConditionalOrNotOutcomeForBothValues(
            Specification<int> first,
            Specification<int> second,
            bool expectedOutcome)
        {
            Specification<int> result = first.OrNot(second);

            Assert.Equal(expectedOutcome, result.IsSatisfiedBy(1));
        }
    }
}