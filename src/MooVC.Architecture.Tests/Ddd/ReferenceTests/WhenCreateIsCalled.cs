﻿namespace MooVC.Architecture.Ddd.ReferenceTests
{
    using System;
    using Xunit;
    using static MooVC.Architecture.Ddd.Reference;

    public sealed class WhenCreateIsCalled
    {
        [Fact]
        public void GivenAnAggregateThenAReferenceIsReturned()
        {
            var aggregate = new SerializableEventCentricAggregateRoot();
            Reference reference = Create(aggregate);

            Assert.Equal(aggregate.Id, reference.Id);
            Assert.Equal(aggregate.GetType(), reference.Type);
            _ = Assert.IsType<Reference<SerializableEventCentricAggregateRoot>>(reference);
        }

        [Fact]
        public void GivenAnAggregateTypeNameThenAReferenceIsReturned()
        {
            Type aggregate = typeof(SerializableEventCentricAggregateRoot);
            var id = Guid.NewGuid();
            Reference reference = Create(aggregate.AssemblyQualifiedName!, id);

            Assert.Equal(id, reference.Id);
            Assert.Equal(aggregate, reference.Type);
            _ = Assert.IsType<Reference<SerializableEventCentricAggregateRoot>>(reference);
        }

        [Fact]
        public void GivenAnAggregateTypeThenAReferenceIsReturned()
        {
            Type aggregate = typeof(SerializableEventCentricAggregateRoot);
            var id = Guid.NewGuid();
            Reference reference = Create(aggregate, id);

            Assert.Equal(id, reference.Id);
            Assert.Equal(aggregate, reference.Type);
            _ = Assert.IsType<Reference<SerializableEventCentricAggregateRoot>>(reference);
        }

        [Fact]
        public void GivenANullAggregateTypeThenAnArgumentNullExceptionIsThrown()
        {
            Type? type = default;
            var id = Guid.NewGuid();

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => Create(type!, id));

            Assert.Equal(nameof(type), exception.ParamName);
        }

        [Fact]
        public void GivenAnNonAggregateTypeNameThenAnArgumentExceptionIsThrown()
        {
            Type aggregate = typeof(Message);
            var id = Guid.NewGuid();

            _ = Assert.Throws<ArgumentException>(
                () => Create(aggregate.AssemblyQualifiedName!, id));
        }

        [Fact]
        public void GivenAnNonAggregateTypeThenAnArgumentExceptionIsThrown()
        {
            Type aggregate = typeof(Message);
            var id = Guid.NewGuid();

            _ = Assert.Throws<ArgumentException>(
                () => Create(aggregate, id));
        }
    }
}