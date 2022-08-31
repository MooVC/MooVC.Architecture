namespace MooVC.Architecture.Ddd.Services.Reconciliation.EventSequenceAdvancedAsyncEventArgsTests;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd.DomainEventTests;
using MooVC.Architecture.MessageTests;
using MooVC.Architecture.Serialization;
using MooVC.Persistence;
using Moq;
using Xunit;

public sealed class WhenEventSequenceAdvancedAsyncEventArgsIsSerialized
{
    [Fact]
    public async Task GivenAnInstanceThenAllPropertiesAreSerializedAsync()
    {
        var eventStore = new Mock<IEventStore<SequencedEvents, ulong>>();
        var reconciler = new Mock<IAggregateReconciler>();
        var instance = new DefaultEventReconciler<SequencedEvents>(eventStore.Object, reconciler.Object);
        EventSequenceAdvancedAsyncEventArgs? original = default;

        _ = eventStore
            .Setup(store => store.ReadAsync(
                It.Is<ulong>(value => value == ulong.MinValue),
                It.IsAny<CancellationToken?>(),
                It.IsAny<ushort>()))
            .ReturnsAsync(new[]
            {
                new SequencedEvents(1, CreateEvents()),
            });

        _ = eventStore
            .Setup(store => store.ReadAsync(
                It.Is<ulong>(value => value > ulong.MinValue),
                It.IsAny<CancellationToken?>(),
                It.IsAny<ushort>()))
            .ReturnsAsync(Enumerable.Empty<SequencedEvents>());

        instance.EventSequenceAdvanced += (sender, e) => Task.FromResult(original = e);

        ulong? current = await instance.ReconcileAsync();

        EventSequenceAdvancedAsyncEventArgs deserialized = original!.Clone();

        Assert.NotSame(original, deserialized);
        Assert.Equal(original!.Sequence, deserialized.Sequence);
    }

    private static DomainEvent[] CreateEvents()
    {
        var aggregate = new SerializableAggregateRoot();
        var context = new SerializableMessage();
        var firstEvent = new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, context);
        var secondEvent = new SerializableDomainEvent<SerializableAggregateRoot>(aggregate, context);

        return new[] { firstEvent, secondEvent };
    }
}