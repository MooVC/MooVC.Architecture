namespace MooVC.Architecture.Ddd.Services.Reconciliation.EventSequenceAdvancedEventArgsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.DomainEventTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Persistence;
    using MooVC.Serialization;
    using Moq;
    using Xunit;

    public sealed class WhenEventSequenceAdvancedEventArgsIsSerialized
    {
        [Fact]
        public async Task GivenAnInstanceThenAllPropertiesAreSerializedAsync()
        {
            var eventStore = new Mock<IEventStore<SequencedEvents, ulong>>();
            var reconciler = new Mock<IAggregateReconciler>();
            var instance = new DefaultEventReconciler<SequencedEvents>(eventStore.Object, reconciler.Object);
            EventSequenceAdvancedEventArgs? original = default;

            _ = eventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value == ulong.MinValue), It.IsAny<ushort>()))
                .ReturnsAsync(new[]
                {
                    new SequencedEvents(1, CreateEvents()),
                });

            _ = eventStore
                .Setup(store => store.ReadAsync(It.Is<ulong>(value => value > ulong.MinValue), It.IsAny<ushort>()))
                .ReturnsAsync(Enumerable.Empty<SequencedEvents>());

            instance.EventSequenceAdvanced += (sender, e) => Task.FromResult(original = e);

            ulong? current = await instance.ReconcileAsync();

            EventSequenceAdvancedEventArgs deserialized = original!.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original!.Sequence, deserialized.Sequence);
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