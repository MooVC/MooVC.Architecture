namespace MooVC.Architecture.Ddd.DomainExceptionTests
{
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenDomainExceptionIsConstructed
    {
        [Fact]
        public void GivenAnAggregateAContextAndAMessageInstanceIsReturnedWithAllPropertiesPropagated()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();
            const string Message = "Something something dark side.";

            var exception = new SerializableDomainException<SerializableEventCentricAggregateRoot>(
                context,
                aggregate,
                Message);

            Assert.Equal(aggregate.ToReference(), exception.Aggregate);
            Assert.Equal(context, exception.Context);
            Assert.Equal(Message, exception.Message);
        }
    }
}