namespace MooVC.Architecture.Services
{
    using System;
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

        public virtual async Task InvokeAsync(Message message)
        {
            ArgumentNotNull(message, nameof(message), BusMessageRequired);

            await OnInvokingAsync(message)
                .ConfigureAwait(false);

            await PerformInvokeAsync(message)
                .ConfigureAwait(false);

            await OnInvokedAsync(message)
                .ConfigureAwait(false);
        }

        protected abstract Task PerformInvokeAsync(Message message);

        protected virtual Task OnDiagnosticsEmittedAsync(
            Level level,
            Exception? cause = default,
            string? message = default)
        {
            return DiagnosticsEmitted.PassiveInvokeAsync(
                this,
                new DiagnosticsEmittedEventArgs(
                    cause: cause,
                    level: level,
                    message: message));
        }

        protected virtual Task OnInvokingAsync(Message message)
        {
            return Invoking.InvokeAsync(this, new MessageInvokingEventArgs(message));
        }

        protected virtual Task OnInvokedAsync(Message message)
        {
            return Invoked.PassiveInvokeAsync(
                this,
                new MessageInvokedEventArgs(message),
                onFailure: failure => OnDiagnosticsEmittedAsync(
                    Level.Warning,
                    cause: failure,
                    message: BusOnInvokedAsyncFailure));
        }
    }
}