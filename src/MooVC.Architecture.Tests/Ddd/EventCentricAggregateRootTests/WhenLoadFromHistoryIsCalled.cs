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
            const ulong ExpectedVersionNumer = 1;

            var original = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();

            IEnumerable<DomainEvent> events = CommitChanges(original, context, times: 1);

            var hydrated = new SerializableEventCentricAggregateRoot(original.Id);

            hydrated.LoadFromHistory(events);

            Assert.Equal(original, hydrated);
            Assert.Equal(original.Value, hydrated.Value);
            Assert.Equal(ExpectedVersionNumer, hydrated.Version.Number);
        }

        [Fact]
        public void GivenEventsStartingFromTheBeginningContainingMultipleVersionsThenTheHydratedAggregateMatchesTheOriginal()
        {
            const ulong ExpectedVersionNumer = 3;

            var original = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();

            IEnumerable<DomainEvent> events = CommitChanges(original, context);

            var hydrated = new SerializableEventCentricAggregateRoot(original.Id);

            hydrated.LoadFromHistory(events);

            Assert.Equal(original, hydrated);
            Assert.Equal(original.Value, hydrated.Value);
            Assert.Equal(ExpectedVersionNumer, hydrated.Version.Number);
        }

        [Fact]
        public void GivenEventsStartingFromAPreviouslyCommittedVersionThenTheHydratedAggregateMatchesTheOriginal()
        {
            const ulong ExpectedVersionNumer = 4;

            var original = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();

            _ = CommitChanges(original, context, times: 1);

            SerializableEventCentricAggregateRoot hydrated = original.Clone();

            IEnumerable<DomainEvent> events = CommitChanges(original, context);

            hydrated.LoadFromHistory(events);

            Assert.Equal(original, hydrated);
            Assert.Equal(original.Value, hydrated.Value);
            Assert.Equal(ExpectedVersionNumer, hydrated.Version.Number);
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