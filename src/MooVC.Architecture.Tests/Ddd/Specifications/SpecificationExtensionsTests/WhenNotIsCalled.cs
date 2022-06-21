namespace MooVC.Architecture.Ddd.Specifications.SpecificationExtensionsTests;

using System;
using System.Collections.Generic;
using Moq;
using Xunit;

public sealed class WhenNotIsCalled
{
    public static IEnumerable<object[]> GivenASpecificationThenTheExpressionResultMatchesTheNegationOfTheSpecificationData()
    {
        var positive = new Mock<Specification<int>>();
        var negative = new Mock<Specification<int>>();

        _ = positive.Setup(spec => spec.ToExpression()).Returns(spec => true);
        _ = negative.Setup(spec => spec.ToExpression()).Returns(spec => false);

        return new[]
        {
            new object[] { negative.Object, true },
            new object[] { positive.Object, false },
        };
    }

    [Fact]
    public void GivenANullSpecificationThenAnArgumentNullExceptionIsThrown()
    {
        _ = Assert.Throws<ArgumentNullException>(() => default(Specification<int>)!.Not());
    }

    [Theory]
    [MemberData(nameof(GivenASpecificationThenTheExpressionResultMatchesTheNegationOfTheSpecificationData))]
    public void GivenASpecificationThenTheExpressionResultMatchesTheNegationOfTheSpecification(Specification<int> specification, bool expectedOutcome)
    {
        Specification<int> result = specification.Not();

        Assert.Equal(expectedOutcome, result.IsSatisfiedBy(1));
    }
}