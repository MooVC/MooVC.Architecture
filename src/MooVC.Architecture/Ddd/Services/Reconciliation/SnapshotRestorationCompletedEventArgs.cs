namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
    using static MooVC.Ensure;

    public sealed class SnapshotRestorationCompletedEventArgs
        : EventArgs
    {
        public SnapshotRestorationCompletedEventArgs(IEventSequence sequence)
        {
            ArgumentNotNull(sequence, nameof(sequence), SnapshotRestorationCompletedEventArgsSequenceRequired);

            Sequence = sequence;
        }

        public IEventSequence Sequence { get; }
    }
}