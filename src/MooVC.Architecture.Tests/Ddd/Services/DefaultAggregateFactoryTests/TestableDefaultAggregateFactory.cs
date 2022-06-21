namespace MooVC.Architecture.Ddd.Services.DefaultAggregateFactoryTests;

using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd;

public sealed class TestableDefaultAggregateFactory
    : DefaultAggregateFactory
{
    public override Task<EventCentricAggregateRoot> CreateAsync(
        Reference aggregate,
        CancellationToken? cancellationToken = default)
    {
        return Task.FromResult<EventCentricAggregateRoot>(
            new SerializableEventCentricAggregateRoot(aggregate.Id));
    }
}