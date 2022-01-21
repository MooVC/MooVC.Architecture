namespace MooVC.Architecture.Ddd.AggregateInvariantsNotSatisfiedDomainExceptionTests
{
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.RequestTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenAggregateInvariantsNotSatisfiedDomainExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var context = new SerializableMessage();
            var request = new TestableRequest(context);
            var aggregate = new SerializableAggregateRoot();

            AggregateInvariantsNotSatisfiedDomainException original = Assert.Throws<AggregateInvariantsNotSatisfiedDomainException>(
                () => request.Satisfies(aggregate, (request => false, "Irrelevant #1")));

            AggregateInvariantsNotSatisfiedDomainException deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Aggregate, deserialized.Aggregate);
            Assert.Equal(original.Explainations, deserialized.Explainations);
        }
    }
}