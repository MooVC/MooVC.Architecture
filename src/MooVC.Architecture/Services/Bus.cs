namespace MooVC.Architecture.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Diagnostics;
using static MooVC.Architecture.Services.Resources;
using static MooVC.Ensure;

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

    public event MessageInvokedAsyncEventHandler? Invoked;

    public event MessageInvokingAsyncEventHandler? Invoking;

    public virtual async Task InvokeAsync(Message message, CancellationToken? cancellationToken = default)
    {
        _ = ArgumentNotNull(message, nameof(message), BusMessageRequired);

        await OnInvokingAsync(message, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        await PerformInvokeAsync(message, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        await OnInvokedAsync(message, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    protected abstract Task PerformInvokeAsync(Message message, CancellationToken? cancellationToken = default);

    protected virtual Task OnDiagnosticsEmittedAsync(
        CancellationToken? cancellationToken = default,
        Exception? cause = default,
        Impact? impact = default,
        Level? level = default,
        string? message = default)
    {
        return diagnostics.EmitAsync(this, cancellationToken: cancellationToken, cause: cause, impact: impact, level: level, message: message);
    }

    protected virtual Task OnInvokingAsync(Message message, CancellationToken? cancellationToken = default)
    {
        return Invoking.InvokeAsync(
            this,
            new MessageInvokingAsyncEventArgs(message, cancellationToken: cancellationToken));
    }

    protected virtual Task OnInvokedAsync(Message message, CancellationToken? cancellationToken = default)
    {
        return Invoked.PassiveInvokeAsync(
            this,
            new MessageInvokedAsyncEventArgs(message, cancellationToken: cancellationToken),
            onFailure: failure => OnDiagnosticsEmittedAsync(
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: BusOnInvokedAsyncFailure));
    }
}