namespace MooVC.Architecture.Ddd.Services.DomainEventsUnhandledEventArgsTests
{
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenDomainEventsUnhandledEventArgsIsConstructed
    {
        [Fact]
        public void GivenEventsAndAHandlerTheAnInstanceIsReturnedWithAllPropertiesSet()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();
            SerializableCreatedDomainEvent[] events = new[] { new SerializableCreatedDomainEvent(context, aggregate) };

            static Task Handler()
            {
                return Task.CompletedTask;
            }

            var @event = new DomainEventsUnhandledEventArgs(events, Handler);

            Assert.Equal(events, @event.Events);
            Assert.Equal(Handler, @event.Handler);
        }
    }
}