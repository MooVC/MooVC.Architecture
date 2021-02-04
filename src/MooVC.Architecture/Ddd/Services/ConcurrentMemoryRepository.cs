namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using MooVC.Serialization;

    public class ConcurrentMemoryRepository<TAggregate>
        : MemoryRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public ConcurrentMemoryRepository(ICloner? cloner = default)
            : base(cloner: cloner)
        {
            StoreLock = new ReaderWriterLockSlim();
        }

        protected virtual ReaderWriterLockSlim StoreLock { get; }

        public override IEnumerable<TAggregate> GetAll()
        {
            return PerformRead(() => base.GetAll());
        }

        protected override TAggregate? Get(Reference key)
        {
            return PerformRead(() => base.Get(key));
        }

        protected override void PerformSave(TAggregate aggregate)
        {
            try
            {
                StoreLock.EnterUpgradeableReadLock();

                if (CheckForConflicts(aggregate))
                {
                    PerformWrite(() => UpdateStore(aggregate));
                }
            }
            finally
            {
                StoreLock.ExitUpgradeableReadLock();
            }
        }

        protected virtual void PerformRead(Action read)
        {
            _ = PerformRead(() =>
              {
                  read();

                  return 0;
              });
        }

        protected virtual T PerformRead<T>(Func<T> read)
        {
            try
            {
                StoreLock.EnterReadLock();

                return read();
            }
            finally
            {
                StoreLock.ExitReadLock();
            }
        }

        protected virtual void PerformWrite(Action write)
        {
            try
            {
                StoreLock.EnterWriteLock();

                write();
            }
            finally
            {
                StoreLock.ExitWriteLock();
            }
        }
    }
}