﻿namespace MooVC.Architecture.Ddd.Services.Snapshots
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.Services.Reconciliation;
    using MooVC.Persistence;

    public sealed class DefaultSnapshotProvider
        : ISnapshotProvider
    {
        private readonly IEventStore<SequencedEvents, ulong> eventStore;
        private readonly Func<Func<Type, IAggregateReconciliationProxy>> factory;
        private readonly ushort numberToRead;

        public DefaultSnapshotProvider(
            IEventStore<SequencedEvents, ulong> eventStore,
            Func<Func<Type, IAggregateReconciliationProxy>> factory,
            ushort numberToRead = DefaultEventReconciler.DefaultNumberToRead)
        {
            this.eventStore = eventStore;
            this.factory = factory;
            this.numberToRead = numberToRead;
        }

        public ISnapshot Generate(ulong? target = null)
        {
            IEventReconciler reconciler = CreateEventReconciler(out Func<IEnumerable<EventCentricAggregateRoot>> aggregates);
            ulong? current = reconciler.Reconcile(target: target);

            if (current.HasValue)
            {
                var sequence = new EventSequence(current.Value);

                return new Snapshot(aggregates(), sequence);
            }

            return default;
        }

        private IEventReconciler CreateEventReconciler(out Func<IEnumerable<EventCentricAggregateRoot>> aggregates)
        {
            var proxies = new ConcurrentDictionary<Type, IAggregateReconciliationProxy>();
            Func<Type, IAggregateReconciliationProxy> external = factory();

            IAggregateReconciliationProxy ProxyFactory(Type aggregate)
            {
                if (!proxies.TryGetValue(aggregate, out IAggregateReconciliationProxy proxy))
                {
                    proxy = external(aggregate);

                    if (proxy is { })
                    {
                        proxies[aggregate] = proxy;
                    }
                }

                return proxy;
            }

            IAggregateReconciler aggregateReconciler = new DefaultAggregateReconciler(ProxyFactory);

            aggregates = () => proxies
                .Values
                .SelectMany(proxy => proxy.GetAll())
                .ToArray();

            return new DefaultEventReconciler(eventStore, aggregateReconciler, numberToRead: numberToRead);
        }
    }
}