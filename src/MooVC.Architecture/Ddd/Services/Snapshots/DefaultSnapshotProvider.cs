namespace MooVC.Architecture.Ddd.Services.Snapshots;

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd.Services.Reconciliation;
using MooVC.Persistence;
using static MooVC.Architecture.Ddd.Services.Snapshots.Resources;
using static MooVC.Ensure;

public sealed class DefaultSnapshotProvider<TSequencedEvents>
    : ISnapshotProvider
    where TSequencedEvents : class, ISequencedEvents
{
    private readonly IEventStore<TSequencedEvents, ulong> eventStore;
    private readonly IAggregateFactory factory;
    private readonly Func<Func<Type, IAggregateReconciliationProxy?>> proxies;
    private readonly ushort numberToRead;

    public DefaultSnapshotProvider(
        IEventStore<TSequencedEvents, ulong> eventStore,
        IAggregateFactory factory,
        Func<Func<Type, IAggregateReconciliationProxy?>> proxies,
        ushort numberToRead = DefaultEventReconciler<TSequencedEvents>.DefaultNumberToRead)
    {
        this.eventStore = ArgumentNotNull(eventStore, nameof(eventStore), DefaultSnapshotProviderEventStoreRequired);
        this.factory = ArgumentNotNull(factory, nameof(factory), DefaultSnapshotProviderFactoryRequired);
        this.proxies = ArgumentNotNull(proxies, nameof(proxies), DefaultSnapshotProviderProxiesRequired);
        this.numberToRead = numberToRead;
    }

    public async Task<ISnapshot?> GenerateAsync(CancellationToken? cancellationToken = default, ulong? target = default)
    {
        IEventReconciler reconciler = CreateEventReconciler(out Func<Task<IEnumerable<EventCentricAggregateRoot>>> aggregates);

        ulong? current = await reconciler
            .ReconcileAsync(cancellationToken: cancellationToken, target: target)
            .ConfigureAwait(false);

        if (current.HasValue)
        {
            var sequence = new EventSequence(current.Value);

            IEnumerable<EventCentricAggregateRoot> values = await aggregates()
                .ConfigureAwait(false);

            return new Snapshot(values, sequence);
        }

        return default;
    }

    private static async Task<IEnumerable<EventCentricAggregateRoot>> RetrieveAllAggregatesAsync(IEnumerable<IAggregateReconciliationProxy> proxies)
    {
        IEnumerable<Task<IEnumerable<EventCentricAggregateRoot>>>? aggregates = proxies
            .Select(async proxy => await proxy.GetAllAsync());

        IEnumerable<EventCentricAggregateRoot>[]? results = await Task.WhenAll(aggregates);

        return results.SelectMany(result => result).ToArray();
    }

    private IEventReconciler CreateEventReconciler(out Func<Task<IEnumerable<EventCentricAggregateRoot>>> aggregates)
    {
        var proxies = new ConcurrentDictionary<Type, IAggregateReconciliationProxy>();
        Func<Type, IAggregateReconciliationProxy?> external = this.proxies();

        IAggregateReconciliationProxy? ProxyFactory(Type aggregate)
        {
            if (!proxies.TryGetValue(aggregate, out IAggregateReconciliationProxy? proxy))
            {
                proxy = external(aggregate);

                if (proxy is { })
                {
                    proxies[aggregate] = proxy;
                }
            }

            return proxy;
        }

        IAggregateReconciler aggregateReconciler = new DefaultAggregateReconciler(factory, ProxyFactory);

        aggregates = () => RetrieveAllAggregatesAsync(proxies.Values);

        return new DefaultEventReconciler<TSequencedEvents>(eventStore, aggregateReconciler, numberToRead: numberToRead);
    }
}