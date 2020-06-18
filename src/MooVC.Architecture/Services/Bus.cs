namespace MooVC.Architecture.Services
{
    using static MooVC.Ensure;
    using static Resources;

    public abstract class Bus
        : IBus
    {
        public event MessageInvokedEventHandler? Invoked;

        public event MessageInvokingEventHandler? Invoking;

        public void Invoke(Message message)
        {
            ArgumentNotNull(message, nameof(message), BusMessageRequired);

            Invoking?.Invoke(this, new MessageInvokingEventArgs(message));

            PerformInvoke(message);

            Invoked?.Invoke(this, new MessageInvokedEventArgs(message));
        }

        protected abstract void PerformInvoke(Message message);
    }
}