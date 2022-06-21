namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Collections.Generic;
using MooVC.Diagnostics;
using static MooVC.Architecture.Ddd.Services.Resources;

public abstract class Bus
    : IBus,
      IEmitDiagnostics
{
    public event DiagnosticsEmittedAsyncEventHandler? DiagnosticsEmitted;

    public event DomainEventsPublishedAsyncEventHandler? Published;

    public event DomainEventsPublishingAsyncEventHandler? Publishing;

    public virtual Task PublishAsync(DomainEvent @event, CancellationToken? cancellationToken = default)
    {
        return PublishAsync(new[] { @event }, cancellationToken: cancellationToken);
    }

    public virtual async Task PublishAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default)
    {
        events = events.Snapshot(predicate: value => value is { });

        if (events.Any())
        {
            await OnPublishingAsync(events, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            await PerformPublishAsync(events, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            await OnPublishedAsync(events, cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }
    }

    protected abstract Task PerformPublishAsync(IEnumerable<DomainEvent> events, CancellationToken? cancellationToken = default);

    protected virtual Task OnDiagnosticsEmittedAsync(
        Level level,
        CancellationToken? cancellationToken = default,
        Exception? cause = default,
        string? message = default)
    {
        return DiagnosticsEmitted.PassiveInvokeAsync(
            this,
            new DiagnosticsEmittedAsyncEventArgs(
                cancellationToken: cancellationToken,
                cause: cause,
                level: level,
                message: message));
    }

    protected virtual Task OnPublishingAsync(IEnumerable<DomainEvent> @events, CancellationToken? cancellationToken = default)
    {
        return Publishing.InvokeAsync(
            this,
            new DomainEventsPublishingAsyncEventArgs(@events, cancellationToken: cancellationToken));
    }

    protected virtual Task OnPublishedAsync(IEnumerable<DomainEvent> @events, CancellationToken? cancellationToken = default)
    {
        return Published.PassiveInvokeAsync(
            this,
            new DomainEventsPublishedAsyncEventArgs(@events, cancellationToken: cancellationToken),
            onFailure: failure => OnDiagnosticsEmittedAsync(
                Level.Warning,
                cancellationToken: cancellationToken,
                cause: failure,
                message: BusOnPublishedAsyncFailure));
    }
}