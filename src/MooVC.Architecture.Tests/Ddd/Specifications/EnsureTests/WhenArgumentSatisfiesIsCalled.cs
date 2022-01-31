namespace MooVC.Architecture.Ddd.Specifications.EnsureTests
{
    using System;
    using Xunit;
    using static MooVC.Architecture.Ddd.Specifications.Ensure;
    using static MooVC.Architecture.Ddd.Specifications.EnsureTests.Resources;
    public sealed class WhenArgumentSatisfiesIsCalled
    {
        [Fact]
        public void GivenAValueTypeWhenTheSpecificationPassesThenTheValueIsReturned()
        {
            const int Expected = 5;

            int actual = ArgumentSatisifies(
                Expected,
                nameof(Expected),
                new PassingValueSpecification());

            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void GivenAValueTypeAndAMessageWhenTheSpecificationPassesThenTheValueIsReturned()
        {
            const int Expected = 5;
            const string Message = "Not going to be seen";

            int actual = ArgumentSatisifies(
                Expected,
                nameof(Expected),
                new PassingValueSpecification(),
                Message);

            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void GivenAValueTypeWhenTheSpecificationFailsThenAnArgumentExceptionIsThrown()
        {
            const int Value = 5;

            ArgumentException exception = Assert.Throws<ArgumentException>(() => ArgumentSatisifies(
                Value,
                nameof(Value),
                new FailingValueSpecification()));

            Assert.Contains(FailingValueSpecification.Requirement, exception.Message);
        }

        [Fact]
        public void GivenAValueTypeAndAMessageWhenTheSpecificationFailsThenAnArgumentExceptionIsThrownWithTheMessage()
        {
            const int Value = 5;
            const string Message = "Will be seen";

            ArgumentException exception = Assert.Throws<ArgumentException>(() => ArgumentSatisifies(
                Value,
                nameof(Value),
                new FailingValueSpecification(),
                Message));

            Assert.Contains(FailingValueSpecification.Requirement, exception.Message);
            Assert.Contains(Message, exception.Message);
        }

        [Fact]
        public void GivenAValueTypeWhenTheSpecificationFailsWithAnEmbeddedMessageThenAnArgumentExceptionIsThrownWithTheMessage()
        {
            const int Value = 5;

            ArgumentException exception = Assert.Throws<ArgumentException>(() => ArgumentSatisifies(
                Value,
                nameof(Value),
                new EmbeddedFailingValueSpecification()));

            Assert.Contains(EmbeddedFailingValueSpecificationRequirement, exception.Message);
        }

        [Fact]
        public void GivenAReferenceTypeWhenTheSpecificationPassesThenTheReferenceIsReturned()
        {
            const string Expected = "Irrelevant value";

            string actual = ArgumentSatisifies(
                Expected,
                nameof(Expected),
                new PassingReferenceSpecification());

            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void GivenAReferenceTypeAndAMessageWhenTheSpecificationPassesThenTheReferenceIsReturned()
        {
            const string Expected = "Irrelevant value";
            const string Message = "Not going to be seen";

            string actual = ArgumentSatisifies(
                Expected,
                nameof(Expected),
                new PassingReferenceSpecification(),
                Message);

            Assert.Equal(Expected, actual);
        }

        [Fact]
        public void GivenAReferenceTypeWhenTheSpecificationFailsThenAnArgumentExceptionIsThrown()
        {
            const string Expected = "Irrelevant value";

            ArgumentException exception = Assert.Throws<ArgumentException>(() => ArgumentSatisifies(
                Expected,
                nameof(Expected),
                new FailingReferenceSpecification()));

            Assert.Contains(FailingReferenceSpecification.Requirement, exception.Message);
        }

        [Fact]
        public void GivenAReferenceTypeAndAMessageWhenTheSpecificationFailsThenAnArgumentExceptionIsThrownWithTheMessage()
        {
            const string Expected = "Irrelevant value";
            const string Message = "Will be seen";

            ArgumentException exception = Assert.Throws<ArgumentException>(() => ArgumentSatisifies(
                Expected,
                nameof(Expected),
                new FailingReferenceSpecification(),
                Message));

            Assert.Contains(FailingReferenceSpecification.Requirement, exception.Message);
            Assert.Contains(Message, exception.Message);
        }

        [Fact]
        public void GivenAReferenceTypeWhenTheSpecificationFailsWithAnEmbeddedMessageThenAnArgumentExceptionIsThrownWithTheMessage()
        {
            const string Expected = "Irrelevant value";

            ArgumentException exception = Assert.Throws<ArgumentException>(() => ArgumentSatisifies(
                Expected,
                nameof(Expected),
                new EmbeddedFailingReferenceSpecification()));

            Assert.Contains(EmbeddedFailingReferenceSpecificationRequirement, exception.Message);
        }
    }
}