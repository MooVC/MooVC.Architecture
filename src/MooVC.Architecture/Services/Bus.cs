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
    public event DiagnosticsEmittedAsyncEventHandler? DiagnosticsEmitted;

    public event MessageInvokedAsyncEventHandler? Invoked;

    public event MessageInvokingAsyncEventHandler? Invoking;

    public virtual async Task InvokeAsync(
        Message message,
        CancellationToken? cancellationToken = default)
    {
        _ = ArgumentNotNull(message, nameof(message), BusMessageRequired);

        await OnInvokingAsync(message, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        await PerformInvokeAsync(message, cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        await OnInvokedAsync(message, cancellationToken: cancellationToken)
            .ConfigureAwait(false);
    }

    protected abstract Task PerformInvokeAsync(
        Message message,
        CancellationToken? cancellationToken = default);

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

    protected virtual Task OnInvokingAsync(
        Message message,
        CancellationToken? cancellationToken = default)
    {
        return Invoking.InvokeAsync(
            this,
            new MessageInvokingAsyncEventArgs(message, cancellationToken: cancellationToken));
    }

    protected virtual Task OnInvokedAsync(
        Message message,
        CancellationToken? cancellationToken = default)
    {
        return Invoked.PassiveInvokeAsync(
            this,
            new MessageInvokedAsyncEventArgs(message, cancellationToken: cancellationToken),
            onFailure: failure => OnDiagnosticsEmittedAsync(
                Level.Warning,
                cancellationToken: cancellationToken,
                cause: failure,
                message: BusOnInvokedAsyncFailure));
    }
}