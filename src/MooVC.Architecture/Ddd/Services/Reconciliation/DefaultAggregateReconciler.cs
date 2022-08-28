namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd;
using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
using static MooVC.Ensure;

public sealed class DefaultAggregateReconciler
    : AggregateReconciler
{
    private readonly IAggregateFactory factory;
    private readonly Func<Type, IAggregateReconciliationProxy?> proxies;
    private readonly bool ignorePreviousVersions;

    public DefaultAggregateReconciler(
        IAggregateFactory factory,
        Func<Type, IAggregateReconciliationProxy?> proxies,
        bool ignorePreviousVersions = true)
    {
        this.factory = ArgumentNotNull(factory, nameof(factory), DefaultAggregateReconcilerFactoryRequired);
        this.proxies = ArgumentNotNull(proxies, nameof(proxies), DefaultAggregateReconcilerProxiesRequired);
        this.ignorePreviousVersions = ignorePreviousVersions;
    }

    public override async Task ReconcileAsync(IEnumerable<EventCentricAggregateRoot> aggregates, CancellationToken? cancellationToken = default)
    {
        if (aggregates.Any())
        {
            foreach (IGrouping<Type, EventCentricAggregateRoot> aggregateTypes in aggregates
                .GroupBy(aggregate => aggregate.GetType()))
            {
                IAggregateReconciliationProxy? proxy = proxies(aggregateTypes.Key);

                if (proxy is null)
                {
                    await OnUnsupportedAggregateTypeDetectedAsync(aggregateTypes.Key, cancellationToken: cancellationToken)
                        .ConfigureAwait(false);
                }
                else
                {
                    foreach (EventCentricAggregateRoot aggregate in aggregateTypes)
                    {
                        await ReconcileAsync(aggregate, proxy, cancellationToken)
                            .ConfigureAwait(false);
                    }
                }
            }
        }
    }

    public override async Task ReconcileAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
    {
        if (events.Any())
        {
            foreach (IGrouping<Type, DomainEvent> aggregateTypes in events
                .GroupBy(@event => @event.Aggregate.Type))
            {
                IAggregateReconciliationProxy? proxy = proxies(aggregateTypes.Key);

                if (proxy is null)
                {
                    await OnUnsupportedAggregateTypeDetectedAsync(aggregateTypes.Key, cancellationToken: cancellationToken)
                        .ConfigureAwait(false);
                }
                else
                {
                    foreach (IGrouping<Reference, DomainEvent> aggregateEvents in aggregateTypes
                        .GroupBy(@event => @event.Aggregate))
                    {
                        bool isHarmonious = await
                            EventsAreNonConflictingAsync(aggregateEvents.Key, aggregateEvents, cancellationToken: cancellationToken)
                            .ConfigureAwait(false);

                        if (isHarmonious)
                        {
                            await ReconcileAsync(aggregateEvents.Key, aggregateEvents, proxy, cancellationToken)
                                .ConfigureAwait(false);
                        }
                    }
                }
            }
        }
    }

    private static Task ReconcileAsync(
        EventCentricAggregateRoot aggregate,
        IAggregateReconciliationProxy proxy,
        CancellationToken? cancellationToken)
    {
        return proxy.OverwriteAsync(aggregate, cancellationToken: cancellationToken);
    }

    private async Task ReconcileAsync(
        Reference aggregate,
        IEnumerable<DomainEvent> events,
        IAggregateReconciliationProxy proxy,
        CancellationToken? cancellationToken)
    {
        EventCentricAggregateRoot? existing = await proxy
             .GetAsync(aggregate, cancellationToken: cancellationToken)
             .ConfigureAwait(false);

        if (existing is null)
        {
            existing = await factory
                .CreateAsync(aggregate, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
        else if (ignorePreviousVersions)
        {
            events = RemovePreviousVersions(events, existing.Version);
        }

        await ApplyAsync(existing, events, proxy, aggregate, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }
}