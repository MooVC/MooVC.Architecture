namespace MooVC.Architecture.Ddd.Services.Reconciliation.EventReconciliationEventArgsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenEventReconciliationEventArgsIsConstructed
    {
        [Fact]
        public void GivenEventsThenAnInstanceIsReturnedWithTheEventsSet()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();
            SerializableCreatedDomainEvent[] events = new[] { new SerializableCreatedDomainEvent(context, aggregate) };
            var @event = new EventReconciliationEventArgs(events);

            Assert.Equal(events, @event.Events);
        }

        [Fact]
        public void GivenEmptyEventsThenAnArgumentExceptionIsThrown()
        {
            IEnumerable<SerializableCreatedDomainEvent> events = Enumerable.Empty<SerializableCreatedDomainEvent>();

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new EventReconciliationEventArgs(events));

            Assert.Equal(nameof(events), exception.ParamName);
        }

        [Fact]
        public void GivenNullEventsThenAnArgumentNullExceptionIsThrown()
        {
            IEnumerable<SerializableCreatedDomainEvent>? events = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new EventReconciliationEventArgs(events!));

            Assert.Equal(nameof(events), exception.ParamName);
        }
    }
}