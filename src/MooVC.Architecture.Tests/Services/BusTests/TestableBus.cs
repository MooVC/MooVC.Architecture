namespace MooVC.Architecture.Services.BusTests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Architecture;

    public sealed class TestableBus
        : Bus
    {
        private readonly bool @throw;

        public TestableBus(bool @throw)
        {
            this.@throw = @throw;
        }

        protected override async Task PerformInvokeAsync(
            Message message,
            CancellationToken? cancellationToken = default)
        {
            if (@throw)
            {
                throw new InvalidOperationException();
            }

            await Task.CompletedTask;
        }
    }
}