namespace MooVC.Architecture.Ddd.VersionedReferenceExtensionsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.Services;
    using Moq;
    using Xunit;

    public sealed class WhenRetrieveIsCalled
    {
        private readonly Mock<Message> context;
        private readonly Mock<IRepository<AggregateRoot>> repository;

        public WhenRetrieveIsCalled()
        {
            context = new Mock<Message>();
            repository = new Mock<IRepository<AggregateRoot>>();
        }

        public static IEnumerable<object[]> GivenOneOrMoreVersionedReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData()
        {
            var reference1 = new VersionedReference<AggregateRoot>(new SerializableAggregateRoot());
            var reference2 = new VersionedReference<AggregateRoot>(new SerializableAggregateRoot());

            yield return new[] { new[] { reference1, reference2, VersionedReference<AggregateRoot>.Empty } };
            yield return new[] { new[] { reference1, VersionedReference<AggregateRoot>.Empty, VersionedReference<AggregateRoot>.Empty } };
            yield return new[] { new[] { VersionedReference<AggregateRoot>.Empty, VersionedReference<AggregateRoot>.Empty, VersionedReference<AggregateRoot>.Empty } };
        }

        public static IEnumerable<object[]> GivenOneOrMoreVersionedReferencesThatDoNotExistsThenAggregateVersionNotFoundExceptionIsThrownForEachThatIsMissingData()
        {
            var reference1 = new VersionedReference<AggregateRoot>(new SerializableAggregateRoot());
            var reference2 = new VersionedReference<AggregateRoot>(new SerializableAggregateRoot());
            var reference3 = new VersionedReference<AggregateRoot>(new SerializableAggregateRoot());

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
            VersionedReference reference = VersionedReference<AggregateRoot>.Empty;

            AggregateDoesNotExistException<AggregateRoot> exception = Assert.Throws<AggregateDoesNotExistException<AggregateRoot>>(
                () => reference.Retrieve(context.Object, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Never);

            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAVersionedReferenceThatDoesNotExistsThenAnAggregateVersionNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(AggregateRoot));

            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<AggregateRoot>(aggregate);

            AggregateVersionNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateVersionNotFoundException<AggregateRoot>>(
                () => reference.Retrieve(context.Object, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(reference, exception.Aggregate);
            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAVersionedReferenceThatDoesNotMatchTheTypeOfTheRepositoryThenAnArgumentExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()))
                .Returns(default(AggregateRoot));

            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<EventCentricAggregateRoot>(aggregate.Id, aggregate.Version);

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.Retrieve(context.Object, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Never);
        }

        [Fact]
        public void GivenAVersionedReferenceThatExistsThenTheAggregateIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = new VersionedReference<AggregateRoot>(aggregate);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregate.Id), It.Is<SignedVersion>(v => v == aggregate.Version)))
               .Returns(aggregate);

            AggregateRoot value = reference.Retrieve(context.Object, repository.Object);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Once);

            Assert.Equal(aggregate, value);
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreVersionedReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData))]
        public void GivenOneOrMoreVersionedReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEach(IEnumerable<VersionedReference> references)
        {
            _ = repository
                .Setup(repo => repo.Get(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<SignedVersion>()))
                .Returns<Guid, SignedVersion>((id, version) => new Mock<AggregateRoot>(id).Object);

            AggregateException exception = Assert.Throws<AggregateException>(
                () => references.Retrieve(context.Object, repository.Object));

            int expected = references.Count(item => item == VersionedReference<AggregateRoot>.Empty);

            repository.Verify(
                repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()),
                Times.Exactly(references.Count() - expected));

            int actual = exception
                .InnerExceptions
                .Cast<AggregateDoesNotExistException<AggregateRoot>>()
                .Count();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreVersionedReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData))]
        public void GivenOneOrMoreVersionedReferencesThatAreEmptyWhenIgnoreEmptyIsTrueThenOnlyTheAggregatesAreReturned(IEnumerable<VersionedReference> references)
        {
            _ = repository
                .Setup(repo => repo.Get(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<SignedVersion>()))
                .Returns<Guid, SignedVersion>((id, version) => new Mock<AggregateRoot>(id).Object);

            IEnumerable<AggregateRoot> results = references.Retrieve(context.Object, repository.Object, ignoreEmpty: true);

            int empties = references.Count(item => item == VersionedReference<AggregateRoot>.Empty);
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
                .Returns<Guid, SignedVersion>((id, version) => new Mock<AggregateRoot>(id).Object);

            AggregateException exception = Assert.Throws<AggregateException>(
                () => references.Keys.Retrieve(context.Object, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<SignedVersion>()), Times.Exactly(references.Count));

            Guid[] expected = references
                .Where(item => !item.Value)
                .Select(item => item.Key.Id)
                .OrderBy(item => item)
                .ToArray();

            Guid[] actual = exception
                .InnerExceptions
                .Cast<AggregateVersionNotFoundException<AggregateRoot>>()
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