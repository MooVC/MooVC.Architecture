namespace MooVC.Architecture.Services
{
    using System.Threading.Tasks;

    public abstract class SynchronousBus
        : Bus
    {
        protected override Task PerformInvokeAsync(Message message)
        {
            PerformInvoke(message);

            return Task.CompletedTask;
        }

        protected abstract void PerformInvoke(Message message);
    }
}