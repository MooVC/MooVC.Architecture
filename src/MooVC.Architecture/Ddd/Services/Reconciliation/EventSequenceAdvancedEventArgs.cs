namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;

    public sealed class EventSequenceAdvancedEventArgs
        : EventArgs
    {
        internal EventSequenceAdvancedEventArgs(ulong sequence)
        {
            Sequence = sequence;
        }

        public ulong Sequence { get; }
    }
}