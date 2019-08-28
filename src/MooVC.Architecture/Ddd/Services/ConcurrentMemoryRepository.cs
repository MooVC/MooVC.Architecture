namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class ConcurrentMemoryRepository<TAggregate>
        : MemoryRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public ConcurrentMemoryRepository()
            : base()
        {
            StoreLock = new ReaderWriterLockSlim();
        }

        protected virtual ReaderWriterLockSlim StoreLock { get; }

        public override IEnumerable<TAggregate> GetAll()
        {
            return PerformRead(() => base.GetAll());
        }

        protected override TAggregate Get(Reference<TAggregate> reference)
        {
            return PerformRead(() => base.Get(reference));
        }

        protected override void PerformSave(TAggregate aggregate, Reference<TAggregate> nonVersioned, Reference<TAggregate> versioned)
        {
            try
            {
                StoreLock.EnterUpgradeableReadLock();

                CheckForConflicts(aggregate, nonVersioned);

                PerformWrite(() => UpdateStore(aggregate, nonVersioned, versioned));
            }
            finally
            {
                StoreLock.ExitUpgradeableReadLock();
            }
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