namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
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

        protected override IEnumerable<TAggregate> PerformGetAll()
        {
            return PerformRead(() => base.PerformGetAll());
        }

        protected override TAggregate? Get(Reference key)
        {
            return PerformRead(() => base.Get(key));
        }

        protected override async Task PerformSaveAsync(TAggregate aggregate)
        {
            try
            {
                StoreLock.EnterUpgradeableReadLock();

                bool isConflictFree = await CheckForConflictsAsync(aggregate)
                    .ConfigureAwait(false);

                if (isConflictFree)
                {
                    PerformWrite(() => PerformUpdateStore(aggregate));
                }
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