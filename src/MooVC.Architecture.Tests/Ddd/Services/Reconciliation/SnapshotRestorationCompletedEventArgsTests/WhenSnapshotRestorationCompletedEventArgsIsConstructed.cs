namespace MooVC.Architecture.Ddd.Services.Reconciliation.SnapshotRestorationCompletedEventArgsTests
{
    using System;
    using Xunit;

    public sealed class WhenSnapshotRestorationCompletedEventArgsIsConstructed
    {
        [Fact]
        public void GivenAnEventSequenceThenAnInstanceIsReturnedWithTheSequencePropagated()
        {
            var sequence = new EventSequence(10);
            var @event = new SnapshotRestorationCompletedEventArgs(sequence);

            Assert.Equal(sequence, @event.Sequence);
        }

        [Fact]
        public void GivenANullEventSequenceThenAnArgumentNullExceptionIsThrown()
        {
            IEventSequence? sequence = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new SnapshotRestorationCompletedEventArgs(sequence!));

            Assert.Equal(nameof(sequence), exception.ParamName);
        }
    }
}