namespace MooVC.Architecture.Ddd.ReferenceExtensionsTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
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
        
        public static IEnumerable<object[]> GivenOneOrMoreReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData()
        {
            var reference1 = new Reference<AggregateRoot>(Guid.NewGuid());
            var reference2 = new Reference<AggregateRoot>(Guid.NewGuid());

            yield return new[] { new[] { reference1, reference2, Reference<AggregateRoot>.Empty } };
            yield return new[] { new[] { reference1, Reference<AggregateRoot>.Empty, Reference<AggregateRoot>.Empty } };
            yield return new[] { new[] { Reference<AggregateRoot>.Empty, Reference<AggregateRoot>.Empty, Reference<AggregateRoot>.Empty } };
        }

        public static IEnumerable<object[]> GivenOneOrMoreReferencesThatDoNotExistsThenAggregateNotFoundExceptionIsThrownForEachThatIsMissingData()
        {
            var reference1 = new Reference<AggregateRoot>(Guid.NewGuid());
            var reference2 = new Reference<AggregateRoot>(Guid.NewGuid());
            var reference3 = new Reference<AggregateRoot>(Guid.NewGuid());

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
                }
            };

            return singles
                .Union(multiples)
                .Union(all)
                .Select(item => new object[] { item });
        }

        [Fact]
        public void GivenAnEmptyReferenceThenAnAggregateDoesNotExistExceptionIsThrown()
        {
            Reference reference = Reference<AggregateRoot>.Empty;

            AggregateDoesNotExistException<AggregateRoot> exception = Assert.Throws<AggregateDoesNotExistException<AggregateRoot>>(
                () => reference.Retrieve(context.Object, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Never);

            Assert.Equal(context.Object, exception.Context);
        }
        
        [Fact]
        public void GivenAReferenceThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()))
                .Returns(default(AggregateRoot));

            var aggregateId = Guid.NewGuid();
            var reference = new Reference<AggregateRoot>(aggregateId);

            AggregateNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateNotFoundException<AggregateRoot>>(
                () => reference.Retrieve(context.Object, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Once);

            Assert.Equal(aggregateId, exception.AggregateId);
            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAReferenceThatDoesNotMatchTheTypeOfTheRepositoryThenAnArgumentExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()))
                .Returns(default(AggregateRoot));

            var aggregateId = Guid.NewGuid();
            var reference = new Reference<EventCentricAggregateRoot>(aggregateId);

            ArgumentException exception = Assert.Throws<ArgumentException>(
                () => reference.Retrieve(context.Object, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Never);
        }

        [Fact]
        public void GivenAReferenceThatExistsThenTheLatestAggregateIsReturned()
        {
            const ulong FirstVersion = 1, 
                SecondVersion = 2;

            var aggregateId = Guid.NewGuid();
            var firstAggregate = new Mock<AggregateRoot>(aggregateId, FirstVersion);
            var secondAggregate = new Mock<AggregateRoot>(aggregateId, SecondVersion);
            var reference = new Reference<AggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId), It.Is<ulong?>(v => v == default)))
               .Returns(secondAggregate.Object);

            AggregateRoot value = reference.Retrieve(context.Object, repository.Object);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Once);

            Assert.Equal(secondAggregate.Object, value);
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData))]
        public void GivenOneOrMoreReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEach(IEnumerable<Reference> references)
        {
            _ = repository
                .Setup(repo => repo.Get(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<ulong?>()))
                .Returns<Guid, ulong?>((id, version) => new Mock<AggregateRoot>(id, AggregateRoot.DefaultVersion).Object);

            AggregateException exception = Assert.Throws<AggregateException>(
                () => references.Retrieve(context.Object, repository.Object));

            int expected = references.Count(item => item == Reference<AggregateRoot>.Empty);

            repository.Verify(
                repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), 
                Times.Exactly(references.Count() - expected));
            
            int actual = exception
                .InnerExceptions
                .Cast<AggregateDoesNotExistException<AggregateRoot>>()
                .Count();

            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [MemberData(nameof(GivenOneOrMoreReferencesThatAreEmptyThenAnAggregateDoesNotExistExceptionIsThrownForEachData))]
        public void GivenOneOrMoreReferencesThatAreEmptyWhenIgnoreEmptyIsTrueThenOnlyTheAggregatesAreReturned(IEnumerable<Reference> references)
        {
            _ = repository
                .Setup(repo => repo.Get(It.Is<Guid>(id => id != Guid.Empty), It.IsAny<ulong?>()))
                .Returns<Guid, ulong?>((id, version) => new Mock<AggregateRoot>(id, AggregateRoot.DefaultVersion).Object);

            IEnumerable<AggregateRoot> results = references.Retrieve(context.Object, repository.Object, ignoreEmpty: true);

            int empties = references.Count(item => item == Reference<AggregateRoot>.Empty);
            int expected = references.Count() - empties;

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Exactly(expected));
            
            Assert.Equal(expected, results.Count());
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreReferencesThatDoNotExistsThenAggregateNotFoundExceptionIsThrownForEachThatIsMissingData))]
        public void GivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissing(IDictionary<Reference, bool> references)
        {
            Expression<Func<Guid, bool>> predicate = id => references
                .Where(item => item.Key.Id == id)
                .Single()
                .Value;

            _ = repository
                .Setup(repo => repo.Get(It.Is(predicate), It.IsAny<ulong?>()))
                .Returns<Guid, ulong?>((id, version) => new Mock<AggregateRoot>(id, AggregateRoot.DefaultVersion).Object);
            
            AggregateException exception = Assert.Throws<AggregateException>(
                () => references.Keys.Retrieve(context.Object, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>(), It.IsAny<ulong?>()), Times.Exactly(references.Count));

            Guid[] expected = references
                .Where(item => !item.Value)
                .Select(item => item.Key.Id)
                .OrderBy(item => item)
                .ToArray();

            Guid[] actual = exception
                .InnerExceptions
                .Cast<AggregateNotFoundException<AggregateRoot>>()
                .Select(aggregate => aggregate.AggregateId)
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