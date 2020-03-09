namespace MooVC.Architecture.Ddd.Services
{
    using MooVC.Processing;

    public abstract class DelayedStartSaga<T>
        : TimedJobQueue<T>,
          IStartSaga<T>
        where T : DomainEvent
    {
        protected DelayedStartSaga(TimedProcessor timer)
            : base(timer)
        {
        }

        public void Start(T @event)
        {
            Enqueue(@event);
        }
    }
}