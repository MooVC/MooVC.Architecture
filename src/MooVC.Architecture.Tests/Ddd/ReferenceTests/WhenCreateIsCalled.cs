namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Xunit;

    public sealed class WhenCreateIsCalled
    {
        [Fact]
        public void GivenAnAggregateTypeNameThenAReferenceIsReturned()
        {
            Type aggregate = typeof(EventCentricAggregateRoot);
            var id = Guid.NewGuid();
            var reference = Reference.Create(aggregate.AssemblyQualifiedName!, id);

            Assert.Equal(id, reference.Id);
            Assert.Equal(aggregate, reference.Type);
            _ = Assert.IsType<Reference<EventCentricAggregateRoot>>(reference);
        }

        [Fact]
        public void GivenAnAggregateTypeThenAReferenceIsReturned()
        {
            Type aggregate = typeof(EventCentricAggregateRoot);
            var id = Guid.NewGuid();
            var reference = Reference.Create(aggregate, id);

            Assert.Equal(id, reference.Id);
            Assert.Equal(aggregate, reference.Type);
            _ = Assert.IsType<Reference<EventCentricAggregateRoot>>(reference);
        }

        [Fact]
        public void GivenAnNonAggregateTypeNameThenAnArgumentExceptionIsThrown()
        {
            Type aggregate = typeof(Message);
            var id = Guid.NewGuid();

            _ = Assert.Throws<ArgumentException>(() => Reference.Create(aggregate.AssemblyQualifiedName!, id));
        }

        [Fact]
        public void GivenAnNonAggregateTypeThenAnArgumentExceptionIsThrown()
        {
            Type aggregate = typeof(Message);
            var id = Guid.NewGuid();

            _ = Assert.Throws<ArgumentException>(() => Reference.Create(aggregate, id));
        }
    }
}