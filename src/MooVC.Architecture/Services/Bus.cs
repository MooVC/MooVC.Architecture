namespace MooVC.Architecture.Services
{
    using System.Threading.Tasks;
    using static MooVC.Architecture.Services.Resources;
    using static MooVC.Ensure;

    public abstract class Bus
        : IBus
    {
        public event MessageInvokedEventHandler? Invoked;

        public event MessageInvokingEventHandler? Invoking;

        public virtual async Task InvokeAsync(Message message)
        {
            ArgumentNotNull(message, nameof(message), BusMessageRequired);

            OnInvoking(message);

            await PerformInvokeAsync(message)
                .ConfigureAwait(false);

            OnInvoked(message);
        }

        protected abstract Task PerformInvokeAsync(Message message);

        protected virtual void OnInvoking(Message message)
        {
            Invoking?.Invoke(this, new MessageInvokingEventArgs(message));
        }

        protected virtual void OnInvoked(Message message)
        {
            Invoked?.Invoke(this, new MessageInvokedEventArgs(message));
        }
    }
}