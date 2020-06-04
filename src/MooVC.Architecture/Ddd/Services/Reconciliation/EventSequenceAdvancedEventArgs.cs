namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    public sealed class EventSequenceAdvancedEventArgs
    {
        public EventSequenceAdvancedEventArgs(ulong sequence)
        {
            Sequence = sequence;
        }

        public ulong Sequence { get; }
    }
}