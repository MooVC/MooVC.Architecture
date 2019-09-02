namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Threading;

    [Serializable]
    public class ConcurrentMemoryRepository<TAggregate>
        : MemoryRepository<TAggregate>
        where TAggregate : AggregateRoot
    {
        public ConcurrentMemoryRepository()
            : base()
        {
            StoreLock = new ReaderWriterLockSlim();
        }

        protected ConcurrentMemoryRepository(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            StoreLock = new ReaderWriterLockSlim();
        }

        protected virtual ReaderWriterLockSlim StoreLock { get; }

        public override IEnumerable<TAggregate> GetAll()
        {
            return PerformRead(() => base.GetAll());
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            PerformRead(() => base.GetObjectData(info, context));
        }

        protected override TAggregate Get(Reference key)
        {
            return PerformRead(() => base.Get(key));
        }

        protected override void PerformSave(TAggregate aggregate, Reference nonVersioned, Reference versioned)
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