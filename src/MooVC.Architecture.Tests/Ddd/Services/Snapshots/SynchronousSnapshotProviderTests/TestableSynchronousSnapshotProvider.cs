namespace MooVC.Architecture.Ddd.Services.Snapshots.SynchronousSnapshotProviderTests
{
    using System;

    public sealed class TestableSynchronousSnapshotProvider
        : SynchronousSnapshotProvider
    {
        private readonly Func<ulong?, ISnapshot?>? generator;

        public TestableSynchronousSnapshotProvider(Func<ulong?, ISnapshot?>? generator = default)
        {
            this.generator = generator;
        }

        protected override ISnapshot? PerformGenerate(ulong? target)
        {
            if (generator is null)
            {
                throw new NotImplementedException();
            }

            return generator(target);
        }
    }
}