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

            Invoking?.Invoke(this, new MessageInvokingEventArgs(message));

            await PerformInvokeAsync(message)
                .ConfigureAwait(false);

            Invoked?.Invoke(this, new MessageInvokedEventArgs(message));
        }

        protected abstract Task PerformInvokeAsync(Message message);
    }
}