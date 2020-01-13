namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Threading;
    using MooVC.Collections.Generic;

    public abstract class DelayedStartSaga<T>
        : IStartSaga<T>,
          IDisposable
        where T : DomainEvent
    {
        private const int ActiveFlag = 1;
        private const int DefaultDelayHours = 0;
        private const int DefaultDelayMinutes = 5;
        private const int DefaultDelaySeconds = 0;
        private const int InactiveFlag = 0;

        private readonly ConcurrentBag<T> events = new ConcurrentBag<T>();
        private readonly Lazy<Timer> timer;
        private int flag = InactiveFlag;

        private bool isDisposed = false;

        protected DelayedStartSaga(TimeSpan? delay = null)
        {
            Delay = delay ?? new TimeSpan(DefaultDelayHours, DefaultDelayMinutes, DefaultDelaySeconds);
            timer = new Lazy<Timer>(() => new Timer(TimerInvoked));
        }

        public bool IsActive => flag == ActiveFlag;

        public TimeSpan Delay { get; }

        public bool HasEventsPending => events.Count > 0;

        protected Timer Timer => timer.Value;

        public void Dispose()
        {
            Dispose(true);
        }

        public void Start(T @event)
        {
            events.Add(@event);

            StartTimer();
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing && timer.IsValueCreated)
                {
                    Timer.Dispose();
                }

                isDisposed = true;
            }
        }

        protected abstract void OnFailureEncountered(Exception failure);

        protected abstract IEnumerable<T> Processing(IEnumerable<T> @events);

        private void StartTimer()
        {
            if (HasEventsPending)
            {
                if (Interlocked.CompareExchange(ref flag, ActiveFlag, InactiveFlag) == InactiveFlag)
                {
                    if (!Timer.Change(Delay, Timeout.InfiniteTimeSpan))
                    {
                        _ = Interlocked.Exchange(ref flag, InactiveFlag);
                    }
                }
            }
        }

        private void TimerInvoked(object state)
        {
            var pending = new List<T>();

            try
            {
                try
                {
                    while (events.TryTake(out T @event))
                    {
                        pending.Add(@event);
                    }

                    Processing(pending).ForEach(@events.Add);
                }
                finally
                {
                    StartTimer();
                }
            }
            catch (Exception failure)
            {
                OnFailureEncountered(failure);
            }
        }
    }
}