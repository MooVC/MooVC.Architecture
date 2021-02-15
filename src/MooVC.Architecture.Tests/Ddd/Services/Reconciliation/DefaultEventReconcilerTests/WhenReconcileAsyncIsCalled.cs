namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultEventReconcilerTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.MessageTests;
    using Moq;
    using Xunit;

    public sealed class WhenReconcileAsyncIsCalled
        : DefaultEventReconcilerTests
    {
        private readonly DefaultEventReconciler<SequencedEvents> instance;

        public WhenReconcileAsyncIsCalled()
        {
            instance = new DefaultEventReconciler<SequencedEvents>(EventStore.Object, Reconciler.Object);
        }

        [Fact]
        public async Task GivenNoSequenceThenAZeroSequenceIsRequestedAsync()
        {
            _ = EventStore
                .Setup(store => store.ReadAsync(It.IsAny<ulong>(), It.IsAny<ushort>()))
                .ReturnsAsync(Enumerable.Empty<SequencedEvents>());

            _ = await instance.ReconcileAsync();

            EventStore.Verify(
                store => store.ReadAsync(It.IsAny<ulong>(), It.IsAny<ushort>()),
                times: Times.Once);

            EventStore.Verify(
                store => store.ReadAsync(It.Is<ulong>(value => value == default), It.IsAny<ushort>()),
                times: Times.Once);
        }

        [Theory]
        [InlineData(ulong.MinValue)]
        [InlineData(5)]
        [InlineData(ulong.MaxValue)]
        public async Task GivenASequenceThenEventsFromThatSequenceAreRequestedAsync(ulong sequence)
        {
            _ = EventStore
                .Setup(store => store.ReadAsync(It.IsAny<ulong>(), It.IsAny<ushort>()))
                .ReturnsAsync(Enumerable.Empty<SequencedEvents>());

            _ = await instance.ReconcileAsync(previous: sequence);

            EventStore.Verify(
                store => store.ReadAsync(It.IsAny<ulong>(), It.IsAny<ushort>()),
                times: Times.Once);

            EventStore.Verify(
                store => store.ReadAsync(It.Is<ulong>(value => value == sequence), It.IsAny<ushort>()),
                times: Times.Once);
        }

        [Theory]
        [InlineData(ulong.MinValue, 1)]
        [InlineData(5, 10)]
        [InlineData(100, 1000)]
        public async Task GivenSequencesThenTheSequenceIsAdvancedToTheHighestSequenceAsync(ulong lowSequence, ulong highSequence)
        {
            bool wasInvoked = false;

            _ = EventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value == ulong.MinValue), It.IsAny<ushort>()))
                .ReturnsAsync(new[]
                {
                    new SequencedEvents(lowSequence, CreateEvents()),
                    new SequencedEvents(highSequence, CreateEvents()),
                });

            _ = EventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value > ulong.MinValue), It.IsAny<ushort>()))
                .ReturnsAsync(Enumerable.Empty<SequencedEvents>());

            instance.EventSequenceAdvanced += (sender, e) => wasInvoked = true;

            ulong? current = await instance.ReconcileAsync();

            Assert.True(wasInvoked);
            Assert.Equal(highSequence, current);
        }

        [Fact]
        public async Task GivenSequencesThenTheReconcilerIsInvokedOnceForEachAggregate()
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

            _ = EventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value == ulong.MinValue), It.IsAny<ushort>()))
                .ReturnsAsync(sequences);

            _ = EventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value > ulong.MinValue), It.IsAny<ushort>()))
                .ReturnsAsync(Enumerable.Empty<SequencedEvents>());

            _ = Reconciler
               .Setup(reconciler => reconciler.ReconcileAsync(It.IsAny<DomainEvent[]>()))
               .Callback<IEnumerable<DomainEvent>>(value => aggregates[value.First().Aggregate]++);

            instance.EventsReconciling += (sender, e) => eventsReconciling++;
            instance.EventsReconciled += (sender, e) => eventsReconciled++;

            _ = await instance.ReconcileAsync();

            Assert.Equal(sequences.Length, eventsReconciling);
            Assert.Equal(sequences.Length, eventsReconciled);
            Assert.All(aggregates.Values, value => Assert.Equal(ExpectedInvocations, value));
        }

        [Fact]
        public async Task GivenSequencesWithAPreviousAndTargetSequenceThenTheReconcilerIsInvokedOnceForEachAggregateAboveThePreviousAndBelowTheTargetSequenceAsync()
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

            ulong previous = 1;
            ulong target = 4;

            SequencedEvents[] expected = sequences
                .Where(aggregate => aggregate.Sequence > previous && aggregate.Sequence <= target)
                .ToArray();

            var aggregates = expected.ToDictionary(sequence => sequence.Aggregate, sequence => 0);

            _ = EventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value == previous), It.IsAny<ushort>()))
                .ReturnsAsync(expected);

            _ = EventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value > previous), It.IsAny<ushort>()))
                .ReturnsAsync(Enumerable.Empty<SequencedEvents>());

            _ = Reconciler
               .Setup(reconciler => reconciler.ReconcileAsync(It.IsAny<DomainEvent[]>()))
               .Callback<IEnumerable<DomainEvent>>(value => aggregates[value.First().Aggregate]++);

            instance.EventsReconciling += (sender, e) => eventsReconciling++;
            instance.EventsReconciled += (sender, e) => eventsReconciled++;

            _ = await instance.ReconcileAsync(previous: previous, target: target);

            int invocations = expected.Length;
            int skips = sequences.Length - invocations;

            Assert.Equal(invocations, eventsReconciling);
            Assert.Equal(invocations, eventsReconciled);
            Assert.All(aggregates.Values, value => Assert.Equal(ExpectedInvocations, value));
        }

        [Fact]
        public async Task GivenSequencesWithAPreviousSequenceThenTheReconcilerIsInvokedOnceForEachAggregateAboveTheLastSequenceAsync()
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

            ulong previous = 2;
            SequencedEvents[] expected = sequences.Where(aggregate => aggregate.Sequence > previous).ToArray();
            var aggregates = expected.ToDictionary(sequence => sequence.Aggregate, sequence => 0);

            _ = EventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value == previous), It.IsAny<ushort>()))
                .ReturnsAsync(expected);

            _ = EventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value > previous), It.IsAny<ushort>()))
                .ReturnsAsync(Enumerable.Empty<SequencedEvents>());

            _ = Reconciler
               .Setup(reconciler => reconciler.ReconcileAsync(It.IsAny<DomainEvent[]>()))
               .Callback<IEnumerable<DomainEvent>>(value => aggregates[value.First().Aggregate]++);

            instance.EventsReconciling += (sender, e) => eventsReconciling++;
            instance.EventsReconciled += (sender, e) => eventsReconciled++;

            _ = await instance.ReconcileAsync(previous: previous);

            int invocations = expected.Length;
            int skips = sequences.Length - invocations;

            Assert.Equal(invocations, eventsReconciling);
            Assert.Equal(invocations, eventsReconciled);
            Assert.All(aggregates.Values, value => Assert.Equal(ExpectedInvocations, value));
        }

        [Fact]
        public async Task GivenSequencesWithATargetSequenceThenTheReconcilerIsInvokedOnceForEachAggregateBelowTheTargetSequenceAsync()
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

            ulong target = 3;
            SequencedEvents[] expected = sequences.Where(aggregate => aggregate.Sequence <= target).ToArray();
            var aggregates = expected.ToDictionary(sequence => sequence.Aggregate, sequence => 0);

            _ = EventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value == ulong.MinValue), It.IsAny<ushort>()))
                .ReturnsAsync(expected);

            _ = EventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value > ulong.MinValue), It.IsAny<ushort>()))
                .ReturnsAsync(Enumerable.Empty<SequencedEvents>());

            _ = Reconciler
               .Setup(reconciler => reconciler.ReconcileAsync(It.IsAny<DomainEvent[]>()))
               .Callback<IEnumerable<DomainEvent>>(value => aggregates[value.First().Aggregate]++);

            instance.EventsReconciling += (sender, e) => eventsReconciling++;
            instance.EventsReconciled += (sender, e) => eventsReconciled++;

            _ = await instance.ReconcileAsync(target: target);

            int invocations = expected.Length;
            int skips = sequences.Length - invocations;

            Assert.Equal(invocations, eventsReconciling);
            Assert.Equal(invocations, eventsReconciled);
            Assert.All(aggregates.Values, value => Assert.Equal(ExpectedInvocations, value));
        }

        private static DomainEvent[] CreateEvents()
        {
            var aggregate = new SerializableAggregateRoot();
            var context = new SerializableMessage();
            var firstEvent = new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate);
            var secondEvent = new SerializableDomainEvent<SerializableAggregateRoot>(context, aggregate);

            return new[] { firstEvent, secondEvent };
        }
    }
}