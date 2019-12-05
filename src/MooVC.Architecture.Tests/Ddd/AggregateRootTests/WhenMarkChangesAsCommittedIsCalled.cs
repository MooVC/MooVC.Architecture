namespace MooVC.Architecture.Ddd.AggregateRootTests
{
    using Xunit;

    public sealed class WhenMarkChangesAsCommittedIsCalled
    {
        [Fact]
        public void GivenAnAggregateWithChangesThenTheChangesMarkedAsCommittedEventIsRaised()
        {
            bool wasInvoked = false;
            var aggregate = new SerializableAggregateRoot();

            aggregate.ChangesMarkedAsCommitted += (sender, e) => wasInvoked = true;
            aggregate.MarkChangesAsCommitted();

            Assert.True(wasInvoked);
        }

        [Fact]
        public void GivenAnAggregateWithNoChangesThenTheChangesMarkedAsCommittedEventIsNotRaised()
        {
            bool wasInvoked = false;
            var aggregate = new SerializableAggregateRoot();

            aggregate.MarkChangesAsCommitted();
            aggregate.ChangesMarkedAsCommitted += (sender, e) => wasInvoked = true;
            aggregate.MarkChangesAsCommitted();

            Assert.False(wasInvoked);
        }
    }
}