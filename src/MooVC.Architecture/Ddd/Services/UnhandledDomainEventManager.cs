namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using MooVC.Collections.Generic;
    using MooVC.Logging;
    using MooVC.Processing;

    public sealed class UnhandledDomainEventManager
        : TimedJobQueue<DomainEventUnhandledEventArgs>,
          IEmitFailures
    {
        private readonly IBus bus;

        public UnhandledDomainEventManager(IBus bus, TimedProcessor timer)
            : base(timer)
        {
            this.bus = bus;

            this.bus.Unhandled += Bus_Unhandled;
        }

        public event PassiveExceptionEventHandler FailureEmitted;

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                bus.Unhandled -= Bus_Unhandled;

                base.Dispose(isDisposing);
            }
        }

        protected override void OnFailureEncountered(Exception failure)
        {
            FailureEmitted?.Invoke(this, new PassiveExceptionEventArgs(failure));
        }

        protected override IEnumerable<DomainEventUnhandledEventArgs> Process(IEnumerable<DomainEventUnhandledEventArgs> jobs)
        {
            var failures = new ConcurrentBag<DomainEventUnhandledEventArgs>();

            jobs.ForAll(job =>
            {
                try
                {
                    job.Handler();
                }
                catch (Exception ex)
                {
                    OnFailureEncountered(ex);

                    failures.Add(job);
                }
            });

            return failures;
        }

        private void Bus_Unhandled(IBus sender, DomainEventUnhandledEventArgs e)
        {
            Enqueue(e);
        }
    }
}
