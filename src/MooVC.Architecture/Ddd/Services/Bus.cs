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
    private readonly IDiagnosticsProxy diagnostics;

    protected Bus(IDiagnosticsProxy? diagnostics = default)
    {
        this.diagnostics = diagnostics ?? new DiagnosticsProxy();
    }

    public event DiagnosticsEmittedAsyncEventHandler DiagnosticsEmitted
    {
        add => diagnostics.DiagnosticsEmitted += value;
        remove => diagnostics.DiagnosticsEmitted -= value;
    }

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
        CancellationToken? cancellationToken = default,
        Exception? cause = default,
        Impact? impact = default,
        Level? level = default,
        string? message = default)
    {
        return diagnostics.EmitAsync(this, cancellationToken: cancellationToken, cause: cause, impact: impact, level: level, message: message);
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
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                level: Level.Warning,
                message: BusOnPublishedAsyncFailure));
    }
}