namespace MooVC.Architecture.Ddd.DomainExceptionTests
{
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenDomainExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            var context = new SerializableMessage();
            const string Message = "Something something dark side.";

            var original = new SerializableDomainException<SerializableEventCentricAggregateRoot>(
                context,
                aggregate,
                Message);

            SerializableDomainException<SerializableEventCentricAggregateRoot> deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Aggregate, deserialized.Aggregate);
            Assert.Equal(original.Context, deserialized.Context);
            Assert.Equal(original.Message, deserialized.Message);
        }
    }
}