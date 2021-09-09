namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using MooVC.Architecture.Ddd.AggregateRootTests;
    using MooVC.Architecture.Ddd.EventCentricAggregateRootTests;
    using MooVC.Architecture.Ddd.Services;
    using MooVC.Architecture.MessageTests;
    using Moq;
    using Xunit;

    public sealed class WhenRetrieveAsyncIsCalled
    {
        private readonly SerializableMessage context;
        private readonly Mock<IRepository<SerializableAggregateRoot>> repository;

        public WhenRetrieveAsyncIsCalled()
        {
            context = new SerializableMessage();
            repository = new Mock<IRepository<SerializableAggregateRoot>>();
        }

        public static IEnumerable<object[]> GivenOneOrMoreReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData()
        {
            var reference1 = new Reference<SerializableAggregateRoot>(Guid.NewGuid());
            var reference2 = new Reference<SerializableAggregateRoot>(Guid.NewGuid());

            yield return new[]
            {
                new[]
                {
                    reference1,
                    reference2,
                    Reference<SerializableAggregateRoot>.Empty,
                },
            };

            yield return new[]
            {
                new[]
                {
                    reference1,
                    Reference<SerializableAggregateRoot>.Empty,
                    Reference<SerializableAggregateRoot>.Empty,
                },
            };

            yield return new[]
            {
                new[]
                {
                    Reference<SerializableAggregateRoot>.Empty,
                    Reference<SerializableAggregateRoot>.Empty,
                    Reference<SerializableAggregateRoot>.Empty,
                },
            };
        }

        public static IEnumerable<object[]> GivenOneOrMoreReferencesThatDoNotExistsThenAggregateNotFoundExceptionIsThrownForEachThatIsMissingData()
        {
            var reference1 = new Reference<SerializableAggregateRoot>(Guid.NewGuid());
            var reference2 = new Reference<SerializableAggregateRoot>(Guid.NewGuid());
            var reference3 = new Reference<SerializableAggregateRoot>(Guid.NewGuid());

            IEnumerable<IDictionary<Reference, bool>> singles = GenerateSinglesForGivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
                reference1,
                reference2,
                reference3);

            IEnumerable<IDictionary<Reference, bool>> multiples = GenerateMultiplesForGivenOneOrMoreReferencesThatDoNotExistsThenAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
                reference1,
                reference2,
                reference3);

            IEnumerable<IDictionary<Reference, bool>> all = new[]
            {
                new Dictionary<Reference, bool>
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
        public async Task GivenAnEmptyReferenceThenAnAggregateDoesNotExistExceptionIsThrownAsync()
        {
            Reference reference = Reference<SerializableAggregateRoot>.Empty;

            AggregateDoesNotExistException<SerializableAggregateRoot> exception =
                await Assert.ThrowsAsync<AggregateDoesNotExistException<SerializableAggregateRoot>>(
                    () => reference.RetrieveAsync(context, repository.Object));

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Never);

            Assert.Equal(context, exception.Context);
        }

        [Fact]
        public async Task GivenAReferenceThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrownAsync()
        {
            _ = repository
                .Setup(repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()))
                .ReturnsAsync(default(SerializableAggregateRoot));

            var aggregateId = Guid.NewGuid();
            var reference = new Reference<SerializableAggregateRoot>(aggregateId);

            AggregateNotFoundException<SerializableAggregateRoot> exception =
                await Assert.ThrowsAsync<AggregateNotFoundException<SerializableAggregateRoot>>(
                    () => reference.RetrieveAsync(context, repository.Object));

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Once);

            Assert.Equal(reference, exception.Aggregate);
            Assert.Equal(context, exception.Context);
        }

        [Fact]
        public async Task GivenAReferenceThatDoesNotMatchTheTypeOfTheRepositoryThenAnArgumentExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()))
                .ReturnsAsync(default(SerializableAggregateRoot));

            var aggregateId = Guid.NewGuid();
            var reference = new Reference<SerializableEventCentricAggregateRoot>(aggregateId);

            ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(
                () => reference.RetrieveAsync(context, repository.Object));

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Never);
        }

        [Fact]
        public async Task GivenAReferenceThatExistsThenTheLatestAggregateIsReturnedAsync()
        {
            var aggregateId = Guid.NewGuid();
            var firstAggregate = new Mock<SerializableAggregateRoot>(aggregateId);
            var secondAggregate = new Mock<SerializableAggregateRoot>(aggregateId);
            var reference = new Reference<SerializableAggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.GetAsync(
                   It.Is<Guid>(id => id == aggregateId),
                   It.IsAny<CancellationToken?>(),
                   It.Is<SignedVersion>(v => v == default)))
               .ReturnsAsync(secondAggregate.Object);

            SerializableAggregateRoot value = await reference.RetrieveAsync(context, repository.Object);

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Once);

            Assert.Equal(secondAggregate.Object, value);
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData))]
        public async Task GivenOneOrMoreReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEach(IEnumerable<Reference> references)
        {
            _ = repository
                .Setup(repo => repo.GetAsync(
                    It.Is<Guid>(id => id != Guid.Empty),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()))
                .ReturnsAsync<Guid, CancellationToken?, SignedVersion?, IRepository<SerializableAggregateRoot>, SerializableAggregateRoot?>(
                    (id, _, version) => new Mock<SerializableAggregateRoot>(id).Object);

            AggregateException exception = await Assert.ThrowsAsync<AggregateException>(
                () => references.RetrieveAsync(context, repository.Object));

            int expected = references.Count(item => item == Reference<SerializableAggregateRoot>.Empty);

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Exactly(references.Count() - expected));

            int actual = exception
                .InnerExceptions
                .Cast<AggregateDoesNotExistException<SerializableAggregateRoot>>()
                .Count();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData))]
        public async Task GivenOneOrMoreReferencesThatAreEmptyWhenIgnoreEmptyIsTrueThenOnlyTheAggregatesAreReturned(IEnumerable<Reference> references)
        {
            _ = repository
                .Setup(repo => repo.GetAsync(
                    It.Is<Guid>(id => id != Guid.Empty),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()))
                .ReturnsAsync<Guid, CancellationToken?, ulong?, IRepository<SerializableAggregateRoot>, SerializableAggregateRoot?>(
                    (id, _, version) => new Mock<SerializableAggregateRoot>(id).Object);

            IEnumerable<SerializableAggregateRoot> results = await references
                .RetrieveAsync(context, repository.Object, ignoreEmpty: true);

            int empties = references.Count(item => item == Reference<SerializableAggregateRoot>.Empty);
            int expected = references.Count() - empties;

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Exactly(expected));

            Assert.Equal(expected, results.Count());
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreReferencesThatDoNotExistsThenAggregateNotFoundExceptionIsThrownForEachThatIsMissingData))]
        public async Task GivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissingAsync(
            IDictionary<Reference, bool> references)
        {
            Expression<Func<Guid, bool>> predicate = id => references
                .Where(item => item.Key.Id == id)
                .Single()
                .Value;

            _ = repository
                .Setup(repo => repo.GetAsync(
                    It.Is(predicate),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()))
                .ReturnsAsync<Guid, CancellationToken?, ulong?, IRepository<SerializableAggregateRoot>, SerializableAggregateRoot?>(
                    (id, _, version) => new Mock<SerializableAggregateRoot>(id).Object);

            AggregateException exception = await Assert.ThrowsAsync<AggregateException>(
                () => references.Keys.RetrieveAsync(context, repository.Object));

            repository.Verify(
                repo => repo.GetAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken?>(),
                    It.IsAny<SignedVersion>()),
                Times.Exactly(references.Count));

            Guid[] expected = references
                .Where(item => !item.Value)
                .Select(item => item.Key.Id)
                .OrderBy(item => item)
                .ToArray();

            Guid[] actual = exception
                .InnerExceptions
                .Cast<AggregateNotFoundException<SerializableAggregateRoot>>()
                .Select(aggregate => aggregate.Aggregate.Id)
                .OrderBy(item => item)
                .ToArray();

            Assert.Equal(expected, actual);
        }

        private static IEnumerable<IDictionary<Reference, bool>> GenerateSinglesForGivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
            Reference reference1,
            Reference reference2,
            Reference reference3)
        {
            yield return new Dictionary<Reference, bool>
            {
                { reference1, false },
                { reference2, true },
                { reference3, true },
            };

            yield return new Dictionary<Reference, bool>
            {
                { reference1, true },
                { reference2, false },
                { reference3, true },
            };

            yield return new Dictionary<Reference, bool>
            {
                { reference1, true },
                { reference2, true },
                { reference3, false },
            };
        }

        private static IEnumerable<IDictionary<Reference, bool>> GenerateMultiplesForGivenOneOrMoreReferencesThatDoNotExistsThenAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
            Reference reference1,
            Reference reference2,
            Reference reference3)
        {
            yield return new Dictionary<Reference, bool>
            {
                { reference1, true },
                { reference2, false },
                { reference3, false },
            };

            yield return new Dictionary<Reference, bool>
            {
                { reference1, false },
                { reference2, true },
                { reference3, false },
            };

            yield return new Dictionary<Reference, bool>
            {
                { reference1, false },
                { reference2, false },
                { reference3, true },
            };
        }
    }
}