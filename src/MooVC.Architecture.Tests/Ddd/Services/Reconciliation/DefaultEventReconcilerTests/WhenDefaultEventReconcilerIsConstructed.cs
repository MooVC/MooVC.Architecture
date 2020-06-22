namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultEventReconcilerTests
{
    using System;
    using Xunit;

    public sealed class WhenDefaultEventReconcilerIsConstructed
        : DefaultEventReconcilerTests
    {
        [Fact]
        public void GivenAnEventStoreAndANullReconcilerThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new DefaultEventReconciler<SequencedEvents>(EventStore.Object, null));
        }

        [Fact]
        public void GivenAReconcilerAndANullEventStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new DefaultEventReconciler<SequencedEvents>(null, Reconciler.Object));
        }

        [Fact]
        public void GivenAnEventStoreAReconcilerThenAnInstanceIsCreated()
        {
            _ = new DefaultEventReconciler<SequencedEvents>(EventStore.Object, Reconciler.Object);
        }
    }
}