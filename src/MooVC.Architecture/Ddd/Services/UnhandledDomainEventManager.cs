namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using MooVC.Collections.Generic;
    using MooVC.Diagnostics;
    using MooVC.Processing;
    using static MooVC.Architecture.Ddd.Services.Resources;

    public sealed class UnhandledDomainEventManager
        : TimedJobQueue<DomainEventsUnhandledEventArgs>
    {
        private readonly IBus bus;

        public UnhandledDomainEventManager(IBus bus, TimedProcessor timer)
            : base(timer)
        {
            this.bus = bus;

            this.bus.Unhandled += Bus_Unhandled;
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                bus.Unhandled -= Bus_Unhandled;

                base.Dispose(isDisposing);
            }
        }

        protected override IEnumerable<DomainEventsUnhandledEventArgs> Process(IEnumerable<DomainEventsUnhandledEventArgs> jobs)
        {
            var failures = new ConcurrentBag<DomainEventsUnhandledEventArgs>();

            jobs.ForAll(job =>
            {
                try
                {
                    job.Handler()
                       .ConfigureAwait(false)
                       .GetAwaiter()
                       .GetResult();
                }
                catch (Exception ex)
                {
                    OnDiagnosticsEmitted(
                        Level.Warning,
                        cause: ex,
                        message: UnhandledDomainEventManagerProcessFailure);

                    failures.Add(job);
                }
            });

            return failures;
        }

        private void Bus_Unhandled(IBus sender, DomainEventsUnhandledEventArgs e)
        {
            Enqueue(e);
        }
    }
}