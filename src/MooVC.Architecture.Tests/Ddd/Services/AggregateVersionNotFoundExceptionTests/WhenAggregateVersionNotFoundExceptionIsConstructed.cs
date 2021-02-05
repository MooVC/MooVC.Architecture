﻿namespace MooVC.Architecture.Ddd.Services.AggregateVersionNotFoundExceptionTests
{
    using System;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.MessageTests;
    using Xunit;

    public sealed class WhenAggregateVersionNotFoundExceptionIsConstructed
    {
        [Fact]
        public void GivenAnAggregateAndAContextThenAnInstanceIsReturnedWithAllPropertiesSet()
        {
            var subject = new SerializableAggregateRoot();
            var aggregate = subject.ToVersionedReference();
            var context = new SerializableMessage();
            var instance = new AggregateVersionNotFoundException<SerializableAggregateRoot>(context, aggregate);

            Assert.Equal(aggregate, instance.Aggregate);
            Assert.Equal(context, instance.Context);
        }

        [Fact]
        public void GivenAnEmptyAggregateAndAContextThenAnArgumentExceptionIsThrown()
        {
            VersionedReference<AggregateRoot> aggregate = VersionedReference<AggregateRoot>.Empty;
            var context = new SerializableMessage();

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new AggregateVersionNotFoundException<AggregateRoot>(context, aggregate));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }

        [Fact]
        public void GivenAnNullAggregateAndAContextThenAnArgumentExceptionIsThrown()
        {
            VersionedReference<AggregateRoot>? aggregate = default;
            var context = new SerializableMessage();

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new AggregateVersionNotFoundException<AggregateRoot>(context, aggregate!));

            Assert.Equal(nameof(aggregate), exception.ParamName);
        }

        [Fact]
        public void GivenAnAggregateAndANullContextThenAnArgumentNullExceptionIsThrown()
        {
            var subject = new SerializableAggregateRoot();
            var aggregate = subject.ToVersionedReference();
            Message? context = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new AggregateVersionNotFoundException<SerializableAggregateRoot>(context!, aggregate));

            Assert.Equal(nameof(context), exception.ParamName);
        }

        [Fact]
        public void GivenAnAggregateIdAContextAndAVersionThenAnInstanceIsReturnedWithAllPropertiesSet()
        {
            var subject = new SerializableAggregateRoot();
            Guid aggregateId = subject.Id;
            var context = new SerializableMessage();
            SignedVersion version = subject.Version;
            var instance = new AggregateVersionNotFoundException<SerializableAggregateRoot>(context, aggregateId, version);

            Assert.Equal(aggregateId, instance.Aggregate.Id);
            Assert.Equal(context, instance.Context);
            Assert.Equal(version, instance.Aggregate.Version);
        }

        [Fact]
        public void GivenAnEmptyAggregateIdAContextAndAVersionThenAnArgumentExceptionIsThrown()
        {
            var subject = new SerializableAggregateRoot();
            Guid id = Guid.Empty;
            var context = new SerializableMessage();
            SignedVersion version = subject.Version;

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => new AggregateVersionNotFoundException<AggregateRoot>(context, id, version));

            Assert.Equal(nameof(id), exception.ParamName);
        }

        [Fact]
        public void GivenAnAggregateIdANullContextAndAVersionThenAnArgumentNullExceptionIsThrown()
        {
            var subject = new SerializableAggregateRoot();
            Guid aggregateId = subject.Id;
            Message? context = default;
            SignedVersion version = subject.Version;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new AggregateVersionNotFoundException<SerializableAggregateRoot>(context!, aggregateId, version));

            Assert.Equal(nameof(context), exception.ParamName);
        }

        [Fact]
        public void GivenAnAggregateIdAContextAndANullVersionThenAnArgumentNullExceptionIsThrown()
        {
            var subject = new SerializableAggregateRoot();
            Guid aggregateId = subject.Id;
            var context = new SerializableMessage();
            SignedVersion? version = default;

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(
                () => new AggregateVersionNotFoundException<SerializableAggregateRoot>(context, aggregateId, version!));

            Assert.Equal(nameof(version), exception.ParamName);
        }
    }
}