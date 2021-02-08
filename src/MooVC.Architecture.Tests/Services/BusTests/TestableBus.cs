namespace MooVC.Architecture.Services.BusTests
{
    using System;
    using MooVC.Architecture;

    public sealed class TestableBus
        : Bus
    {
        private readonly bool @throw;

        public TestableBus(bool @throw)
        {
            this.@throw = @throw;
        }

        protected override void PerformInvoke(Message message)
        {
            if (@throw)
            {
                throw new InvalidOperationException();
            }
        }
    }
}