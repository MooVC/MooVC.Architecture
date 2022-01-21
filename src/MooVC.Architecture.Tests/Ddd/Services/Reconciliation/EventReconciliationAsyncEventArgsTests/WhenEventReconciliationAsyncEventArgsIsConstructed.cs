namespace MooVC.Architecture.Ddd.Services.Reconciliation.EventReconciliationAsyncEventArgsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenEventReconciliationAsyncEventArgsIsConstructed
    {
        [Fact]
        public void GivenEventsThenAnInstanceIsReturnedWithTheEventsSet()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();
            SerializableCreatedDomainEvent[] events = new[] { new SerializableCreatedDomainEvent(context, aggregate) };
            var @event = new EventReconciliationAsyncEventArgs(events);

            Assert.Equal(events, @event.Events);
        }

        [Fact]
        public void GivenEmptyEventsThenAnArgumentExceptionIsThrown()
        {
            IEnumerable<SerializableCreatedDomainEvent> events = Enumerable.Empty<SerializableCreatedDomainEvent>();

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new EventReconciliationAsyncEventArgs(events));

            Assert.Equal(nameof(events), exception.ParamName);
        }

        [Fact]
        public void GivenNullEventsThenAnArgumentNullExceptionIsThrown()
        {
            IEnumerable<SerializableCreatedDomainEvent>? events = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new EventReconciliationAsyncEventArgs(events!));

            Assert.Equal(nameof(events), exception.ParamName);
        }
    }
}