namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;
    using System.Linq;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenMarkChangesAsCommittedIsCalled
    {
        [Fact]
        public void GivenAnAggregateWithChangesThenTheChangesMarkedAsCommittedEventIsRaisedWithTheChangesAttached()
        {
            bool wasInvoked = false;
            var aggregate = new SerializableEventCentricAggregateRoot(Guid.NewGuid());
            var context = new SerializableMessage();
            var request = new SetRequest(context, Guid.NewGuid());

            aggregate.Set(request);

            aggregate.ChangesMarkedAsCommitted += (sender, e) =>
            {
                var changes = e as ChangesMarkedAsCommittedEventArgs;

                Assert.NotNull(changes);
                Assert.True(changes.Changes.Count() == 1);
                _ = Assert.IsType<SerializableSetDomainEvent>(changes.Changes.First());

                wasInvoked = true;
            };

            aggregate.MarkChangesAsCommitted();

            Assert.True(wasInvoked);
        }

        [Fact]
        public void GivenAnAggregateWithNoChangesThenTheChangesMarkedAsCommittedEventIsNotRaised()
        {
            bool wasInvoked = false;
            var aggregate = new SerializableEventCentricAggregateRoot(Guid.NewGuid());

            aggregate.MarkChangesAsCommitted();

            aggregate.ChangesMarkedAsCommitted += (sender, e) => wasInvoked = true;
            aggregate.MarkChangesAsCommitted();

            Assert.False(wasInvoked);
        }
    }
}