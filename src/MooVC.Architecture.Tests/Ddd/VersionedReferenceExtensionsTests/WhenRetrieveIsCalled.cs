namespace MooVC.Architecture.Ddd.VersionedReferenceExtensionsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services;
    using MooVC.Architecture.MessageTests;
    using Moq;
    using Xunit;

    public sealed class WhenRetrieveIsCalled
    {
        private readonly SerializableMessage context;
        private readonly Mock<IRepository<SerializableAggregateRoot>> repository;

        public WhenRetrieveIsCalled()
        {
            context = new SerializableMessage();
            repository = new Mock<IRepository<SerializableAggregateRoot>>();
        }

        public static IEnumerable<object[]> GivenOneOrMoreVersionedReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData()
        {
            var reference1 = new VersionedReference<SerializableAggregateRoot>(new SerializableAggregateRoot());
            var reference2 = new VersionedReference<SerializableAggregateRoot>(new SerializableAggregateRoot());

            yield return new[]
            {
                new[]
                {
                    reference1,
                    reference2,
                    VersionedReference<SerializableAggregateRoot>.Empty,
                },
            };

            yield return new[]
            {
                new[]
                {
                    reference1,
                    VersionedReference<SerializableAggregateRoot>.Empty,
                    VersionedReference<SerializableAggregateRoot>.Empty,
                },
            };

            yield return new[]
            {
                new[]
                {
                    VersionedReference<SerializableAggregateRoot>.Empty,
                    VersionedReference<SerializableAggregateRoot>.Empty,
                    VersionedReference<SerializableAggregateRoot>.Empty,
                },
            };
        }

        public static IEnumerable<object[]> GivenOneOrMoreVersionedReferencesThatDoNotExistsThenAggregateVersionNotFoundExceptionIsThrownForEachThatIsMissingData()
        {
            var reference1 = new VersionedReference<SerializableAggregateRoot>(new SerializableAggregateRoot());
            var reference2 = new VersionedReference<SerializableAggregateRoot>(new SerializableAggregateRoot());
            var reference3 = new VersionedReference<SerializableAggregateRoot>(new SerializableAggregateRoot());

            IEnumerable<IDictionary<VersionedReference, bool>> singles = GenerateSinglesForGivenOneOrMoreVersionedReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
                reference1,
                reference2,
                reference3);

            IEnumerable<IDictionary<VersionedReference, bool>> multiples = GenerateMultiplesForGivenOneOrMoreVersionedReferencesThatDoNotExistsThenAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
                reference1,
                reference2,
                reference3);

            IEnumerable<IDictionary<VersionedReference, bool>> all = new[]
            {
                new Dictionary<VersionedReference, bool>
                {
                    { reference1, false },
                    { reference2, false },
                    { reference3, false },
                },
            };

            return singles
                .Union(multiples)
                .Union(all)
                .Select(item => new object[] { item });
        }

        [Fact]
        public void GivenAnEmptyVersionedReferenceThenAnAggregateDoesNotExistExceptionIsThrown()
        {
            VersionedReference reference = VersionedReference<SerializableAggregateRoot>.Empty;

            AggregateDoesNotExistException<SerializableAggregateRoot> exception =
                Assert.Throws<AggregateDoesNotExistException<SerializableAggregateRoot>>(
                    () => reference.Retrieve(context, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Never);

            Assert.Equal(context, exception.Context);
        }

        [Fact]
        public void GivenAVersionedReferenceThatDoesNotExistsThenAnAggregateVersionNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(SerializableAggregateRoot));

            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<SerializableAggregateRoot>(aggregate);

            AggregateVersionNotFoundException<SerializableAggregateRoot> exception =
                Assert.Throws<AggregateVersionNotFoundException<SerializableAggregateRoot>>(
                    () => reference.Retrieve(context, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(reference, exception.Aggregate);
            Assert.Equal(context, exception.Context);
        }

        [Fact]
        public void GivenAVersionedReferenceThatDoesNotMatchTheTypeOfTheRepositoryThenAnArgumentExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(SerializableAggregateRoot));

            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<SerializableEventCentricAggregateRoot>(aggregate.Id, aggregate.Version);

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.Retrieve(context, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Never);
        }

        [Fact]
        public void GivenAVersionedReferenceThatExistsThenTheAggregateIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<SerializableAggregateRoot>(aggregate);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregate.Id), It.Is<SignedVersion>(v => v == aggregate.Version)))
               .Returns(aggregate);

            SerializableAggregateRoot value = reference.Retrieve(context, repository.Object);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(aggregate, value);
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreVersionedReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData))]
        public void GivenOneOrMoreVersionedReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEach(IEnumerable<VersionedReference> references)
        {
            _ = repository
                .Setup(repo => repo.Get(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<SignedVersion>()))
                .Returns<Guid, SignedVersion>((id, version) => new SerializableAggregateRoot(id));

            AggregateException exception = Assert.Throws<AggregateException>(
                () => references.Retrieve(context, repository.Object));

            int expected = references.Count(item => item == VersionedReference<SerializableAggregateRoot>.Empty);

            repository.Verify(
                repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()),
                Times.Exactly(references.Count() - expected));

            int actual = exception
                .InnerExceptions
                .Cast<AggregateDoesNotExistException<SerializableAggregateRoot>>()
                .Count();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreVersionedReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData))]
        public void GivenOneOrMoreVersionedReferencesThatAreEmptyWhenIgnoreEmptyIsTrueThenOnlyTheAggregatesAreReturned(IEnumerable<VersionedReference> references)
        {
            _ = repository
                .Setup(repo => repo.Get(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<SignedVersion>()))
                .Returns<Guid, SignedVersion>((id, version) => new SerializableAggregateRoot(id));

            IEnumerable<AggregateRoot> results = references.Retrieve(context, repository.Object, ignoreEmpty: true);

            int empties = references.Count(item => item == VersionedReference<SerializableAggregateRoot>.Empty);
            int expected = references.Count() - empties;

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Exactly(expected));

            Assert.Equal(expected, results.Count());
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreVersionedReferencesThatDoNotExistsThenAggregateVersionNotFoundExceptionIsThrownForEachThatIsMissingData))]
        public void GivenOneOrMoreVersionedReferencesThatDoNotExistsThenAnAggregateVersionNotFoundExceptionIsThrownForEachThatIsMissing(IDictionary<VersionedReference, bool> references)
        {
            Expression<Func<Guid, bool>> predicate = id => references
                .Where(item => item.Key.Id == id)
                .Single()
                .Value;

            _ = repository
                .Setup(repo => repo.Get(It.Is(predicate), It.IsAny<SignedVersion>()))
                .Returns<Guid, SignedVersion>((id, version) => new SerializableAggregateRoot(id));

            AggregateException exception = Assert.Throws<AggregateException>(
                () => references.Keys.Retrieve(context, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Exactly(references.Count));

            Guid[] expected = references
                .Where(item => !item.Value)
                .Select(item => item.Key.Id)
                .OrderBy(item => item)
                .ToArray();

            Guid[] actual = exception
                .InnerExceptions
                .Cast<AggregateVersionNotFoundException<SerializableAggregateRoot>>()
                .Select(exception => exception.Aggregate.Id)
                .OrderBy(item => item)
                .ToArray();

            Assert.Equal(expected, actual);
        }

        private static IEnumerable<IDictionary<VersionedReference, bool>> GenerateSinglesForGivenOneOrMoreVersionedReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
            VersionedReference reference1,
            VersionedReference reference2,
            VersionedReference reference3)
        {
            yield return new Dictionary<VersionedReference, bool>
            {
                { reference1, false },
                { reference2, true },
                { reference3, true },
            };

            yield return new Dictionary<VersionedReference, bool>
            {
                { reference1, true },
                { reference2, false },
                { reference3, true },
            };

            yield return new Dictionary<VersionedReference, bool>
            {
                { reference1, true },
                { reference2, true },
                { reference3, false },
            };
        }

        private static IEnumerable<IDictionary<VersionedReference, bool>> GenerateMultiplesForGivenOneOrMoreVersionedReferencesThatDoNotExistsThenAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
            VersionedReference reference1,
            VersionedReference reference2,
            VersionedReference reference3)
        {
            yield return new Dictionary<VersionedReference, bool>
            {
                { reference1, true },
                { reference2, false },
                { reference3, false },
            };

            yield return new Dictionary<VersionedReference, bool>
            {
                { reference1, false },
                { reference2, true },
                { reference3, false },
            };

            yield return new Dictionary<VersionedReference, bool>
            {
                { reference1, false },
                { reference2, false },
                { reference3, true },
            };
        }
    }
}