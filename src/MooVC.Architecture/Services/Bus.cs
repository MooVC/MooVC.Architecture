namespace MooVC.Architecture.Services;

using System.Threading;
using System.Threading.Tasks;
using MooVC.Diagnostics;
using static MooVC.Architecture.Services.Resources;
using static MooVC.Ensure;

public abstract class Bus
    : IBus,
      IEmitDiagnostics
{
    protected Bus(IDiagnosticsProxy? diagnostics = default)
    {
        Diagnostics = new DiagnosticsRelay(this, diagnostics: diagnostics);
    }

    public event DiagnosticsEmittedAsyncEventHandler DiagnosticsEmitted
    {
        add => Diagnostics.DiagnosticsEmitted += value;
        remove => Diagnostics.DiagnosticsEmitted -= value;
    }

    public event MessageInvokedAsyncEventHandler? Invoked;

    public event MessageInvokingAsyncEventHandler? Invoking;

    protected IDiagnosticsRelay Diagnostics { get; }

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
            onFailure: failure => Diagnostics.EmitAsync(
                cancellationToken: cancellationToken,
                cause: failure,
                impact: Impact.None,
                message: BusOnInvokedAsyncFailure));
    }
}