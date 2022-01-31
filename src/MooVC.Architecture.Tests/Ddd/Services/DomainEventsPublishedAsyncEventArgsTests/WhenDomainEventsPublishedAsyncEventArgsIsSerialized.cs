namespace MooVC.Architecture.Ddd.Services.DomainEventsPublishedAsyncEventArgsTests
{
    using System.Collections.Generic;
    using MooVC.Architecture.Ddd.Services.DomainEventsAsyncEventArgsTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenDomainEventsPublishedAsyncEventArgsIsSerialized
        : DomainEventsAsyncEventArgsBase
    {
        [Fact]
        public void GivenNullEventsThenAllPropertiesAreSerialized()
        {
            var @event = new DomainEventsPublishedAsyncEventArgs(default!);
            DomainEventsPublishedAsyncEventArgs deserialized = @event.Clone();

            Assert.NotSame(@event, deserialized);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void GivenEventsThenAllPropertiesAreSerialized(int count)
        {
            IEnumerable<DomainEvent> events = CreateEvents(count);
            var @event = new DomainEventsPublishedAsyncEventArgs(events);

            DomainEventsPublishedAsyncEventArgs deserialized = @event.Clone();

            Assert.NotSame(@event, deserialized);
        }
    }
}