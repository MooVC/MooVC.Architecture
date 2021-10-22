﻿namespace MooVC.Architecture.Ddd.ProjectionTests
{
    using System;
    using Xunit;

    public sealed class WhenEqualityIsChecked
    {
        [Fact]
        public void GivenAMatchingProjectionThenAPositiveResponseIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var first = new SerializableProjection<SerializableAggregateRoot>(aggregate);
            var second = new SerializableProjection<SerializableAggregateRoot>(aggregate);

            Assert.True(first == second);
        }

        [Fact]
        public void GivenAMatchingReferenceThenAPositiveResponseIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var first = new SerializableProjection<SerializableAggregateRoot>(aggregate);
            var second = aggregate.ToReference();

            Assert.True(first == second);
        }

        [Fact]
        public void GivenAMismatchingProjectionThenANegativeResponseIsReturned()
        {
            var first = new SerializableProjection<SerializableAggregateRoot>(
                new SerializableAggregateRoot());

            var second = new SerializableProjection<SerializableAggregateRoot>(
                new SerializableAggregateRoot());

            Assert.False(first == second);
        }

        [Fact]
        public void GivenAMismatchingReferenceThenANegativeResponseIsReturned()
        {
            var first = new SerializableProjection<SerializableAggregateRoot>(
                new SerializableAggregateRoot());

            var second = Reference.Create<SerializableAggregateRoot>(Guid.NewGuid());

            Assert.False(first == second);
        }
    }
}