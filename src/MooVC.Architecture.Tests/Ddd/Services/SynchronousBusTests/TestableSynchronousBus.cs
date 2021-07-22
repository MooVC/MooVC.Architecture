namespace MooVC.Architecture.Ddd.Services.SynchronousBusTests
{
    using System;
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd;

    public sealed class TestableSynchronousBus
        : SynchronousBus
    {
        private readonly Action<IEnumerable<DomainEvent>>? publish;

        public TestableSynchronousBus(Action<IEnumerable<DomainEvent>>? publish = default)
        {
            this.publish = publish;
        }

        protected override void PerformPublish(IEnumerable<DomainEvent> events)
        {
            if (publish is null)
            {
                throw new NotImplementedException();
            }

            publish(events);
        }
    }
}