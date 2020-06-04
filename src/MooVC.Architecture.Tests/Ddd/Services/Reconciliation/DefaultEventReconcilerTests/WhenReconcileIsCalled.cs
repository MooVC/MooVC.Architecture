namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultEventReconcilerTests
{
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Linq;
    using Moq;
    using Xunit;

    public sealed class WhenReconcileIsCalled
        : DefaultEventReconcilerTests
    {
        private readonly DefaultEventReconciler instance;

        public WhenReconcileIsCalled()
        {
            instance = new DefaultEventReconciler(EventStore.Object, Reconciler.Object, SequenceStore.Object);
        }

        [Fact]
        public void GivenNoSequenceThenAZeroSequenceIsRequested()
        {
            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new EventSequence[0]);

            _ = EventStore
                .Setup(store => store.Read(It.IsAny<ulong>(), It.IsAny<ushort>()))
                .Returns(new SequencedEvents[0]);

            _ = instance.Reconcile();

            EventStore.Verify(store => store.Read(It.IsAny<ulong>(), It.IsAny<ushort>()), times: Times.Once);
            EventStore.Verify(store => store.Read(It.Is<ulong>(value => value == default), It.IsAny<ushort>()), times: Times.Once);
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(5)]
        [InlineData(ulong.MaxValue)]
        public void GivenASequenceThenEventsFromThatSequenceAreRequested(ulong sequence)
        {
            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new[] { new EventSequence(sequence) });

            _ = EventStore
                .Setup(store => store.Read(It.IsAny<ulong>(), It.IsAny<ushort>()))
                .Returns(new SequencedEvents[0]);

            _ = instance.Reconcile();

            EventStore.Verify(store => store.Read(It.IsAny<ulong>(), It.IsAny<ushort>()), times: Times.Once);
            EventStore.Verify(store => store.Read(It.Is<ulong>(value => value == sequence), It.IsAny<ushort>()), times: Times.Once);
        }

        [Theory]
        [InlineData(ulong.MinValue, 1)]
        [InlineData(5, 10)]
        [InlineData(100, 1000)]
        public void GivenSequencesThenTheSequenceIsAdvancedToTheHighestSequence(ulong lowSequence, ulong highSequence)
        {
            bool wasInvoked = false;

            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new EventSequence[0]);

            _ = EventStore
                .Setup(store => store.Read(It.Is<ulong>(value => value == ulong.MinValue), It.IsAny<ushort>()))
                .Returns(new[]
                {
                    new SequencedEvents(lowSequence, CreateEvents()),
                    new SequencedEvents(highSequence, CreateEvents()),
                });

            _ = EventStore
                .Setup(store => store.Read(It.Is<ulong>(value => value > ulong.MinValue), It.IsAny<ushort>()))
                .Returns(new SequencedEvents[0]);

            instance.EventSequenceAdvanced += (sender, e) => wasInvoked = true;

            IEventSequence current = instance.Reconcile();

            Assert.True(wasInvoked);
            Assert.Equal(highSequence, current.Sequence);

            SequenceStore.Verify(store => store.Create(It.IsAny<EventSequence>()), times: Times.Once);
            SequenceStore.Verify(store => store.Create(It.Is<EventSequence>(value => value.Sequence == highSequence)), times: Times.Once);
        }

        [Fact]
        public void GivenSequencesThenTheReconcilerIsInvokedOnceForEachAggregate()
        {
            const int ExpectedInvocations = 1;
            int eventsReconciling = 0;
            int eventsReconciled = 0;

            SequencedEvents[] sequences = new[]
            {
                new SequencedEvents(1, CreateEvents()),
                new SequencedEvents(2, CreateEvents()),
                new SequencedEvents(3, CreateEvents()),
            };

            var aggregates = sequences.ToDictionary(sequence => sequence.Aggregate, sequence => 0);

            _ = SequenceStore
                .Setup(store => store.Get(It.IsAny<Paging>()))
                .Returns(new EventSequence[0]);

            _ = EventStore
                .Setup(store => store.Read(It.Is<ulong>(value => value == ulong.MinValue), It.IsAny<ushort>()))
                .Returns(sequences);

            _ = EventStore
                .Setup(store => store.Read(It.Is<ulong>(value => value > ulong.MinValue), It.IsAny<ushort>()))
                .Returns(new SequencedEvents[0]);

            _ = Reconciler
               .Setup(reconciler => reconciler.Reconcile(It.IsAny<IEnumerable<DomainEvent>>()))
               .Callback<IEnumerable<DomainEvent>>(value => aggregates[value.First().Aggregate]++);

            instance.EventsReconciling += (sender, e) => eventsReconciling++;
            instance.EventsReconciled += (sender, e) => eventsReconciled++;

            _ = instance.Reconcile();

            Assert.Equal(sequences.Count(), eventsReconciling);
            Assert.Equal(sequences.Count(), eventsReconciled);
            Assert.All(aggregates.Values, value => Assert.Equal(ExpectedInvocations, value));
        }

        [Fact]
        public void GivenSequencesWithAPreviousAndTargetSequenceThenTheReconcilerIsInvokedOnceForEachAggregateAboveThePreviousAndBelowTheTargetSequence()
        {
            const int ExpectedInvocations = 1;
            int eventsReconciling = 0;
            int eventsReconciled = 0;

            SequencedEvents[] sequences = new[]
            {
                new SequencedEvents(1, CreateEvents()),
                new SequencedEvents(2, CreateEvents()),
                new SequencedEvents(3, CreateEvents()),
                new SequencedEvents(4, CreateEvents()),
                new SequencedEvents(5, CreateEvents()),
            };

            var previous = new EventSequence(1);
            var target = new EventSequence(4);

            SequencedEvents[] expected = sequences
                .Where(aggregate => aggregate.Sequence > previous.Sequence && aggregate.Sequence <= target.Sequence)
                .ToArray();

            var aggregates = expected.ToDictionary(sequence => sequence.Aggregate, sequence => 0);

            _ = EventStore
                .Setup(store => store.Read(It.Is<ulong>(value => value == previous.Sequence), It.IsAny<ushort>()))
                .Returns(expected);

            _ = EventStore
                .Setup(store => store.Read(It.Is<ulong>(value => value > previous.Sequence), It.IsAny<ushort>()))
                .Returns(new SequencedEvents[0]);

            _ = Reconciler
               .Setup(reconciler => reconciler.Reconcile(It.IsAny<IEnumerable<DomainEvent>>()))
               .Callback<IEnumerable<DomainEvent>>(value => aggregates[value.First().Aggregate]++);

            instance.EventsReconciling += (sender, e) => eventsReconciling++;
            instance.EventsReconciled += (sender, e) => eventsReconciled++;

            _ = instance.Reconcile(previous: previous, target: target);

            int invocations = expected.Count();
            int skips = sequences.Count() - invocations;

            Assert.Equal(invocations, eventsReconciling);
            Assert.Equal(invocations, eventsReconciled);
            Assert.All(aggregates.Values, value => Assert.Equal(ExpectedInvocations, value));

            SequenceStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Never);
            SequenceStore.Verify(store => store.Create(It.IsAny<EventSequence>()), times: Times.Once);
        }

        [Fact]
        public void GivenSequencesWithAPreviousSequenceThenTheReconcilerIsInvokedOnceForEachAggregateAboveTheLastSequence()
        {
            const int ExpectedInvocations = 1;
            int eventsReconciling = 0;
            int eventsReconciled = 0;

            SequencedEvents[] sequences = new[]
            {
                new SequencedEvents(1, CreateEvents()),
                new SequencedEvents(2, CreateEvents()),
                new SequencedEvents(3, CreateEvents()),
                new SequencedEvents(4, CreateEvents()),
                new SequencedEvents(7, CreateEvents()),
            };

            var previous = new EventSequence(2);
            SequencedEvents[] expected = sequences.Where(aggregate => aggregate.Sequence > previous.Sequence).ToArray();
            var aggregates = expected.ToDictionary(sequence => sequence.Aggregate, sequence => 0);

            _ = EventStore
                .Setup(store => store.Read(It.Is<ulong>(value => value == previous.Sequence), It.IsAny<ushort>()))
                .Returns(expected);

            _ = EventStore
                .Setup(store => store.Read(It.Is<ulong>(value => value > previous.Sequence), It.IsAny<ushort>()))
                .Returns(new SequencedEvents[0]);

            _ = Reconciler
               .Setup(reconciler => reconciler.Reconcile(It.IsAny<IEnumerable<DomainEvent>>()))
               .Callback<IEnumerable<DomainEvent>>(value => aggregates[value.First().Aggregate]++);

            instance.EventsReconciling += (sender, e) => eventsReconciling++;
            instance.EventsReconciled += (sender, e) => eventsReconciled++;

            _ = instance.Reconcile(previous: previous);

            int invocations = expected.Count();
            int skips = sequences.Count() - invocations;

            Assert.Equal(invocations, eventsReconciling);
            Assert.Equal(invocations, eventsReconciled);
            Assert.All(aggregates.Values, value => Assert.Equal(ExpectedInvocations, value));

            SequenceStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Never);
            SequenceStore.Verify(store => store.Create(It.IsAny<EventSequence>()), times: Times.Once);
        }

        [Fact]
        public void GivenSequencesWithATargetSequenceThenTheReconcilerIsInvokedOnceForEachAggregateBelowTheTargetSequence()
        {
            const int ExpectedInvocations = 1;
            int eventsReconciling = 0;
            int eventsReconciled = 0;

            SequencedEvents[] sequences = new[]
            {
                new SequencedEvents(1, CreateEvents()),
                new SequencedEvents(2, CreateEvents()),
                new SequencedEvents(3, CreateEvents()),
                new SequencedEvents(4, CreateEvents()),
                new SequencedEvents(5, CreateEvents()),
            };

            var target = new EventSequence(3);
            SequencedEvents[] expected = sequences.Where(aggregate => aggregate.Sequence <= target.Sequence).ToArray();
            var aggregates = expected.ToDictionary(sequence => sequence.Aggregate, sequence => 0);

            _ = EventStore
                .Setup(store => store.Read(It.Is<ulong>(value => value == ulong.MinValue), It.IsAny<ushort>()))
                .Returns(expected);

            _ = EventStore
                .Setup(store => store.Read(It.Is<ulong>(value => value > ulong.MinValue), It.IsAny<ushort>()))
                .Returns(new SequencedEvents[0]);

            _ = Reconciler
               .Setup(reconciler => reconciler.Reconcile(It.IsAny<IEnumerable<DomainEvent>>()))
               .Callback<IEnumerable<DomainEvent>>(value => aggregates[value.First().Aggregate]++);

            instance.EventsReconciling += (sender, e) => eventsReconciling++;
            instance.EventsReconciled += (sender, e) => eventsReconciled++;

            _ = instance.Reconcile(target: target);

            int invocations = expected.Count();
            int skips = sequences.Count() - invocations;

            Assert.Equal(invocations, eventsReconciling);
            Assert.Equal(invocations, eventsReconciled);
            Assert.All(aggregates.Values, value => Assert.Equal(ExpectedInvocations, value));

            SequenceStore.Verify(store => store.Get(It.IsAny<Paging>()), times: Times.Once);
            SequenceStore.Verify(store => store.Create(It.IsAny<EventSequence>()), times: Times.Once);
        }

        private DomainEvent[] CreateEvents()
        {
            var version = new SerializableAggregateRoot().ToVersionedReference();
            var context = new SerializableMessage();
            var firstEvent = new SerializableDomainEvent(context, version);
            var secondEvent = new SerializableDomainEvent(context, version);

            return new[] { firstEvent, secondEvent };
        }
    }
}