namespace MooVC.Architecture.Ddd.Services
{
    using static MooVC.Ensure;
    using static Resources;

    public sealed class EventSequenceAdvancedEventArgs
    {
        internal EventSequenceAdvancedEventArgs(IEventSequence sequence)
        {
            ArgumentNotNull(sequence, nameof(sequence), EventSequenceAdvancedEventArgsSequenceRequired);

            Sequence = sequence;
        }

        public IEventSequence Sequence { get; }
    }
}