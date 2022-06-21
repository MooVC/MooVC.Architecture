namespace MooVC.Architecture.Ddd.Specifications.SpecificationExtensionsTests;

using System;
using System.Collections.Generic;
using Moq;
using Xunit;

public sealed class WhenAndIsCalled
{
    public static IEnumerable<object?[]> GivenANullSpecificationThenAnArgumentNullExceptionIsThrownData()
    {
        var specification = new Mock<Specification<int>>();

        return new[]
        {
            new object?[] { default, default },
            new object?[] { default, specification.Object },
            new object?[] { specification.Object, default },
        };
    }

    public static IEnumerable<object[]> GivenTwoSpecificationsThenTheExpressionResultMatchesTheConditionalAndOutcomeForBothValuesData()
    {
        var positive = new Mock<Specification<int>>();
        var negative = new Mock<Specification<int>>();

        _ = positive.Setup(spec => spec.ToExpression()).Returns(spec => true);
        _ = negative.Setup(spec => spec.ToExpression()).Returns(spec => false);

        return new[]
        {
            new object[] { negative.Object, negative.Object, false },
            new object[] { negative.Object, positive.Object, false },
            new object[] { positive.Object, negative.Object, false },
            new object[] { positive.Object, positive.Object, true },
        };
    }

    [Theory]
    [MemberData(nameof(GivenANullSpecificationThenAnArgumentNullExceptionIsThrownData))]
    public void GivenANullSpecificationThenAnArgumentNullExceptionIsThrown(Specification<int>? first, Specification<int>? second)
    {
        _ = Assert.Throws<ArgumentNullException>(() => first!.And(second!));
    }

    [Theory]
    [MemberData(nameof(GivenTwoSpecificationsThenTheExpressionResultMatchesTheConditionalAndOutcomeForBothValuesData))]
    public void GivenTwoSpecificationsThenTheExpressionResultMatchesTheConditionalAndOutcomeForBothValues(
        Specification<int> first,
        Specification<int> second,
        bool expectedOutcome)
    {
        Specification<int> result = first.And(second);

        Assert.Equal(expectedOutcome, result.IsSatisfiedBy(1));
    }
}