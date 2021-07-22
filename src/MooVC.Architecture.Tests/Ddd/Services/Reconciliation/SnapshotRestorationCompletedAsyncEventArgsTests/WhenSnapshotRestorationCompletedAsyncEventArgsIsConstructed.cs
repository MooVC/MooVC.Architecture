namespace MooVC.Architecture.Ddd.Services.Reconciliation.SnapshotRestorationCompletedAsyncEventArgsTests
{
    using System;
    using Xunit;

    public sealed class WhenSnapshotRestorationCompletedAsyncEventArgsIsConstructed
    {
        [Fact]
        public void GivenAnEventSequenceThenAnInstanceIsReturnedWithTheSequencePropagated()
        {
            var sequence = new EventSequence(10);
            var @event = new SnapshotRestorationCompletedAsyncEventArgs(sequence);

            Assert.Equal(sequence, @event.Sequence);
        }

        [Fact]
        public void GivenANullEventSequenceThenAnArgumentNullExceptionIsThrown()
        {
            IEventSequence? sequence = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new SnapshotRestorationCompletedAsyncEventArgs(sequence!));

            Assert.Equal(nameof(sequence), exception.ParamName);
        }
    }
}