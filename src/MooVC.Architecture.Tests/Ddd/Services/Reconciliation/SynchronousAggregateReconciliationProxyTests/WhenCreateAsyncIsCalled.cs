namespace MooVC.Architecture.Ddd.Services.Reconciliation.SynchronousAggregateReconciliationProxyTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using Xunit;

    public sealed class WhenCreateAsyncIsCalled
    {
        [Fact]
        public async Task GivenAReferenceThenTheReferenceIsPropagatedAsync()
        {
            bool wasInvoked = false;
            var expectedAggregate = new SerializableEventCentricAggregateRoot();
            var expectedReference = expectedAggregate.ToReference();

            var proxy = new TestableSynchronousAggregateReconciliationProxy(create: actualReference =>
            {
                wasInvoked = true;

                Assert.Equal(expectedReference, actualReference);

                return expectedAggregate;
            });

            EventCentricAggregateRoot? actualAggregate = await proxy.CreateAsync(expectedReference);

            Assert.True(wasInvoked);
            Assert.Equal(expectedAggregate, actualAggregate);
        }

        [Fact]
        public async Task GivenNoAggregateThenTheExceptionIsThrownAsync()
        {
            var reconciler = new TestableSynchronousAggregateReconciliationProxy();

            _ = await Assert.ThrowsAsync<NotImplementedException>(
                () => reconciler.CreateAsync(default(Reference)!));
        }

        [Fact]
        public async Task GivenNoDomainEventsThenAnExceptionIsThrownAsync()
        {
            var reconciler = new TestableSynchronousAggregateReconciliationProxy();

            _ = await Assert.ThrowsAsync<DomainEventsMissingException>(
                () => reconciler.CreateAsync(Enumerable.Empty<DomainEvent>()));
        }

        [Fact]
        public async Task GivenNullDomainEventsThenAnExceptionIsThrownAsync()
        {
            var reconciler = new TestableSynchronousAggregateReconciliationProxy();

            _ = await Assert.ThrowsAsync<DomainEventsMissingException>(
                () => reconciler.CreateAsync(default(IEnumerable<DomainEvent>)!));
        }
    }
}