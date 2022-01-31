namespace MooVC.Architecture.RequestExtensionsTests
{
    using System;
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.RequestTests;
    using Moq;
    using Xunit;

    public sealed class WhenSatisfiesIsCalled
    {
        [Fact]
        public void GivenAnAggregateAndNoInvariantsThenNoExceptionIsThrown()
        {
            var context = new SerializableMessage();
            var request = new TestableRequest(context);
            var aggregate = new SerializableAggregateRoot();

            request.Satisfies(aggregate);
        }

        [Fact]
        public void GivenAnAggregateAndSeriesOfPassingInvariantsThenNoExceptionIsThrown()
        {
            var context = new SerializableMessage();
            var request = new TestableRequest(context);
            var aggregate = new SerializableAggregateRoot();

            request.Satisfies(
                aggregate,
                (request => true, "Irrelevant #1"),
                (request => true, "Irrelevant #2"));
        }

        [Fact]
        public void GivenAnAggregateAndSeriesOfFailingInvariantsThenAnAggregateInvariantsNotSatisfiedDomainExceptionIsThrownThatContainsTheExplainations()
        {
            const string ExpectedMessage1 = "Expected #1";
            const string ExpectedMessage2 = "Expected #2";
            const string IrrelevantMessage1 = "Irrelevant #1";
            const string IrrelevantMessage2 = "Irrelevant #2";

            var context = new SerializableMessage();
            var request = new TestableRequest(context);
            var aggregate = new SerializableAggregateRoot();

            AggregateInvariantsNotSatisfiedDomainException exception = Assert.Throws<AggregateInvariantsNotSatisfiedDomainException>(
                () => request.Satisfies(
                    aggregate,
                    (request => false, ExpectedMessage1),
                    (request => true, IrrelevantMessage1),
                    (request => false, ExpectedMessage2),
                    (request => true, IrrelevantMessage2)));

            Assert.Contains(ExpectedMessage1, exception.Message);
            Assert.Contains(ExpectedMessage2, exception.Message);
            Assert.DoesNotContain(IrrelevantMessage1, exception.Message);
            Assert.DoesNotContain(IrrelevantMessage2, exception.Message);
        }

        [Fact]
        public void GivenAFactoryAndNoInvariantsThenTheFactoryIsNotInvoked()
        {
            var context = new SerializableMessage();
            var request = new TestableRequest(context);
            bool wasInvoked = false;

            DomainException Factory(IEnumerable<string> explainations)
            {
                wasInvoked = true;

                return default!;
            }

            request.Satisfies(Factory);

            Assert.False(wasInvoked);
        }

        [Fact]
        public void GivenAFactoryAndSeriesOfPassingInvariantsThenTheFactoryIsNotInvoked()
        {
            var context = new SerializableMessage();
            var request = new TestableRequest(context);
            bool wasInvoked = false;

            DomainException Factory(IEnumerable<string> explainations)
            {
                wasInvoked = true;

                return default!;
            }

            request.Satisfies(
                Factory,
                (request => true, "Irrelevant #1"),
                (request => true, "Irrelevant #2"));

            Assert.False(wasInvoked);
        }

        [Fact]
        public void GivenAFactoryAndSeriesOfFailingInvariantsThenTheFactoryIsInvokedAndTheResultThrown()
        {
            const string ExpectedMessage1 = "Expected #1";
            const string ExpectedMessage2 = "Expected #2";
            const string IrrelevantMessage1 = "Irrelevant #1";
            const string IrrelevantMessage2 = "Irrelevant #2";

            var context = new SerializableMessage();
            var request = new TestableRequest(context);
            var aggregate = new SerializableAggregateRoot();
            var expected = new Mock<DomainException<SerializableAggregateRoot>>(context, aggregate, string.Empty);

            bool wasInvoked = false;

            DomainException Factory(IEnumerable<string> explainations)
            {
                wasInvoked = true;

                Assert.Contains(ExpectedMessage1, explainations);
                Assert.Contains(ExpectedMessage2, explainations);
                Assert.DoesNotContain(IrrelevantMessage1, explainations);
                Assert.DoesNotContain(IrrelevantMessage2, explainations);

                return expected.Object;
            }

            Exception actual = Assert.Throws(
                expected.Object.GetType(),
                () => request.Satisfies(
                    Factory,
                    (request => false, ExpectedMessage1),
                    (request => true, IrrelevantMessage1),
                    (request => false, ExpectedMessage2),
                    (request => true, IrrelevantMessage2)));

            Assert.True(wasInvoked);
            Assert.Equal(expected.Object, actual);
        }

        [Fact]
        public void GivenAVersionedReferenceAndNoInvariantsThenNoExceptionIsThrown()
        {
            var context = new SerializableMessage();
            var request = new TestableRequest(context);
            var aggregate = new SerializableAggregateRoot();

            request.Satisfies(aggregate.ToReference());
        }

        [Fact]
        public void GivenAVersionedReferenceAndSeriesOfPassingInvariantsThenNoExceptionIsThrown()
        {
            var context = new SerializableMessage();
            var request = new TestableRequest(context);
            var aggregate = new SerializableAggregateRoot();

            request.Satisfies(
                aggregate.ToReference(),
                (request => true, "Irrelevant #1"),
                (request => true, "Irrelevant #2"));
        }

        [Fact]
        public void GivenAVersionedReferenceAndSeriesOfFailingInvariantsThenAnAggregateInvariantsNotSatisfiedDomainExceptionIsThrownThatContainsTheExplainations()
        {
            const string ExpectedMessage1 = "Expected #1";
            const string ExpectedMessage2 = "Expected #2";
            const string IrrelevantMessage1 = "Irrelevant #1";
            const string IrrelevantMessage2 = "Irrelevant #2";

            var context = new SerializableMessage();
            var request = new TestableRequest(context);
            var aggregate = new SerializableAggregateRoot();

            AggregateInvariantsNotSatisfiedDomainException exception = Assert.Throws<AggregateInvariantsNotSatisfiedDomainException>(
                () => request.Satisfies(
                    aggregate.ToReference(),
                    (request => false, ExpectedMessage1),
                    (request => true, IrrelevantMessage1),
                    (request => false, ExpectedMessage2),
                    (request => true, IrrelevantMessage2)));

            Assert.Contains(ExpectedMessage1, exception.Message);
            Assert.Contains(ExpectedMessage2, exception.Message);
            Assert.DoesNotContain(IrrelevantMessage1, exception.Message);
            Assert.DoesNotContain(IrrelevantMessage2, exception.Message);
        }
    }
}