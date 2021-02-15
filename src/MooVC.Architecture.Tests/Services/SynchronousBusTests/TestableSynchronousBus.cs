namespace MooVC.Architecture.Services.SynchronousBusTests
{
    using System;
    using MooVC.Architecture;

    public sealed class TestableSynchronousBus
        : SynchronousBus
    {
        private readonly Action<Message>? invoke;

        public TestableSynchronousBus(Action<Message>? invoke = default)
        {
            this.invoke = invoke;
        }

        protected override void PerformInvoke(Message message)
        {
            if (invoke is null)
            {
                throw new NotImplementedException();
            }

            invoke(message);
        }
    }
}