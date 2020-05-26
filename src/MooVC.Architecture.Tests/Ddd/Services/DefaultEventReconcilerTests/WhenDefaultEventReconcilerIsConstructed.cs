namespace MooVC.Architecture.Ddd.Services.DefaultEventReconcilerTests
{
    using System;
    using Xunit;

    public sealed class WhenDefaultEventReconcilerIsConstructed
        : DefaultEventReconcilerTests
    {
        [Fact]
        public void GivenAnEventStoreAReconcilerAndANullSequenceStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new DefaultEventReconciler(EventStore.Object, Reconciler.Object, null));
        }

        [Fact]
        public void GivenAnEventStoreASequenceStoreAndANullReconcilerThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new DefaultEventReconciler(EventStore.Object, null, SequenceStore.Object));
        }

        [Fact]
        public void GivenAReconcilerAndASequenceStoreAndANullEventStoreThenAnArgumentNullExceptionIsThrown()
        {
            _ = Assert.Throws<ArgumentNullException>(
                () => new DefaultEventReconciler(null, Reconciler.Object, SequenceStore.Object));
        }

        [Fact]
        public void GivenAnEventStoreAReconcilerAndASequenceStoreThenAnInstanceIsCreated()
        {
            _ = new DefaultEventReconciler(EventStore.Object, Reconciler.Object, SequenceStore.Object);
        }
    }
}