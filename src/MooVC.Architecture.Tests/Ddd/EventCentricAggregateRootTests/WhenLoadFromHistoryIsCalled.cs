namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests
{
    using System;
    using System.Collections.Generic;
    using MooVC.Architecture.MessageTests;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenLoadFromHistoryIsCalled
    {
        [Fact]
        public void GivenEventsStartingFromTheBeginningThenTheHydratedAggregateMatchesTheOriginal()
        {
            var original = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();

            IEnumerable<DomainEvent> events = CommitChanges(original, context, times: 1);

            var hydrated = new SerializableEventCentricAggregateRoot(original.Id);

            hydrated.LoadFromHistory(events);

            Assert.Equal(original, hydrated);
            Assert.Equal(original.Value, hydrated.Value);
        }

        [Fact]
        public void GivenEventsStartingFromTheBeginningContainingMultipleVersionsThenTheHydratedAggregateMatchesTheOriginal()
        {
            var original = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();

            IEnumerable<DomainEvent> events = CommitChanges(original, context);

            var hydrated = new SerializableEventCentricAggregateRoot(original.Id);

            hydrated.LoadFromHistory(events);

            Assert.Equal(original, hydrated);
            Assert.Equal(original.Value, hydrated.Value);
        }

        [Fact]
        public void GivenEventsStartingFromAPreviouslyCommittedVersionThenTheHydratedAggregateMatchesTheOriginal()
        {
            var original = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();

            _ = CommitChanges(original, context, times: 1);

            SerializableEventCentricAggregateRoot hydrated = original.Clone();

            IEnumerable<DomainEvent> events = CommitChanges(original, context);

            hydrated.LoadFromHistory(events);

            Assert.Equal(original, hydrated);
            Assert.Equal(original.Value, hydrated.Value);
        }

        private static IEnumerable<DomainEvent> CommitChanges(
            SerializableEventCentricAggregateRoot original,
            SerializableMessage context,
            int times = 3)
        {
            var events = new List<DomainEvent>();

            for (int index = 0; index < times; index++)
            {
                original.Set(new SetRequest(context, Guid.NewGuid()));

                events.AddRange(original.GetUncommittedChanges());

                original.MarkChangesAsCommitted();
            }

            return events;
        }
    }
}