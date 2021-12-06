namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using Xunit;

    public sealed class WhenImplicityCastToReference
    {
        [Fact]
        public void GivenAnAggregateThenAReferenceForThatAggregateIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            Reference reference = aggregate;

            Assert.True(reference.IsMatch(aggregate));
        }

        [Fact]
        public void GivenANullAggregateThenAnEmptyReferenceIsReturned()
        {
            SerializableAggregateRoot? aggregate = default;
            Reference reference = aggregate;

            Assert.True(reference.IsEmpty);
        }
    }
}