namespace MooVC.Architecture.Ddd.Services.AggregateNotFoundExceptionTests
{
    using System;
    using MooVC.Architecture.MessageTests;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenAggregateNotFoundExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var aggregate = Guid.NewGuid().ToReference<AggregateRoot>();
            var context = new SerializableMessage();

            var original = new AggregateNotFoundException<AggregateRoot>(
                context,
                aggregate);

            AggregateNotFoundException<AggregateRoot> deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.Equal(original.Aggregate, deserialized.Aggregate);
            Assert.Equal(original.Context, deserialized.Context);
        }
    }
}