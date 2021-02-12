namespace MooVC.Architecture.Services
{
    using System.Threading.Tasks;

    public abstract class SynchronousBus
        : Bus
    {
        protected override async Task PerformInvokeAsync(Message message)
        {
            PerformInvoke(message);

            await Task.CompletedTask;
        }

        protected abstract void PerformInvoke(Message message);
    }
}