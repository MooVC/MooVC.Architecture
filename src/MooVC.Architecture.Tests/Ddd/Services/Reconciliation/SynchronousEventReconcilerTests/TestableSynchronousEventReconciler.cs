namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousEventReconcilerTests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd;
using MooVC.Architecture.MessageTests;

public sealed class TestableSynchronousEventReconciler
    : SynchronousEventReconciler
{
    private readonly Action<IEnumerable<DomainEvent>>? events;

    public TestableSynchronousEventReconciler(Action<IEnumerable<DomainEvent>>? events = default)
    {
        this.events = events;
    }

    protected override async Task<(ulong? LastSequence, IEnumerable<DomainEvent> Events)> GetEventsAsync(
        ulong? previous,
        CancellationToken? cancellationToken = default,
        ulong? target = default)
    {
        if (previous.GetValueOrDefault() == 0)
        {
            SerializableCreatedDomainEvent[]? events = new[]
            {
                new SerializableCreatedDomainEvent(new SerializableEventCentricAggregateRoot(), new SerializableMessage()),
            };

            return await Task.FromResult((ulong.MaxValue, events));
        }

        return await Task.FromResult((ulong.MaxValue, Enumerable.Empty<DomainEvent>()));
    }

    protected override void PerformReconcile(IEnumerable<DomainEvent> events)
    {
        if (this.events is null)
        {
            throw new NotImplementedException();
        }

        this.events(events);
    }
}