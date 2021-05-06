namespace MooVC.Architecture.Ddd.Services.SynchronousBusTests
{
    using System;
    using MooVC.Architecture.Ddd;

    public sealed class TestableSynchronousBus
        : SynchronousBus
    {
        private readonly Action<DomainEvent[]>? publish;

        public TestableSynchronousBus(Action<DomainEvent[]>? publish = default)
        {
            this.publish = publish;
        }

        protected override void PerformPublish(params DomainEvent[] events)
        {
            if (publish is null)
            {
                throw new NotImplementedException();
            }

            publish(events);
        }
    }
}