namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System.Threading;
    using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
    using static MooVC.Ensure;

    public sealed class SnapshotRestorationCompletedAsyncEventArgs
        : AsyncEventArgs
    {
        public SnapshotRestorationCompletedAsyncEventArgs(
            IEventSequence sequence,
            CancellationToken? cancellationToken = default)
            : base(cancellationToken: cancellationToken)
        {
            ArgumentNotNull(sequence, nameof(sequence), SnapshotRestorationCompletedEventArgsSequenceRequired);

            Sequence = sequence;
        }

        public IEventSequence Sequence { get; }
    }
}