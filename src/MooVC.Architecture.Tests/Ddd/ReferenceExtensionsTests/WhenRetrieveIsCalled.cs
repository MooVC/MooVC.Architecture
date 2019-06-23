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

        public static IEnumerable<object[]> GivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissingData()
        {
            var reference1 = new Reference<AggregateRoot>(Guid.NewGuid());
            var reference2 = new Reference<AggregateRoot>(Guid.NewGuid());
            var reference3 = new Reference<AggregateRoot>(Guid.NewGuid());

            IEnumerable<IDictionary<Reference<AggregateRoot>, bool>> singles = GenerateSinglesForGivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
                reference1,
                reference2,
                reference3);

            IEnumerable<IDictionary<Reference<AggregateRoot>, bool>> multiples = GenerateMultiplesForGivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
                reference1,
                reference2,
                reference3);

            IEnumerable<IDictionary<Reference<AggregateRoot>, bool>> all = new[]
            {
                new Dictionary<Reference<AggregateRoot>, bool>
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
        public void GivenAReferenceThatDoesNotExistsThenAnAggregateNotFoundExceptionIsThrown()
        {
            _ = repository
                .Setup(repo => repo.Get(It.IsAny<Guid>()))
                .Returns(default(AggregateRoot));

            var aggregateId = Guid.NewGuid();
            var reference = new Reference<AggregateRoot>(aggregateId);
            
            AggregateNotFoundException<AggregateRoot> exception = Assert.Throws<AggregateNotFoundException<AggregateRoot>>(
                () => reference.Retrieve(context.Object, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>()), Times.Once);

            Assert.Equal(aggregateId, exception.AggregateId);
            Assert.Equal(context.Object, exception.Context);
        }

        [Fact]
        public void GivenAReferenceThatExistsThenTheAggregateIsReturned()
        {
            var aggregateId = Guid.NewGuid();
            var aggregate = new Mock<AggregateRoot>(aggregateId, AggregateRoot.DefaultVersion);
            var reference = new Reference<AggregateRoot>(aggregateId);

            _ = repository
               .Setup(repo => repo.Get(It.Is<Guid>(id => id == aggregateId)))
               .Returns(aggregate.Object);

            AggregateRoot value = reference.Retrieve(context.Object, repository.Object);

            repository.Verify(repo => repo.Get(It.IsAny<Guid>()), Times.Once);

            Assert.Equal(aggregate.Object, value);
        }

        [Theory]
        [MemberData(nameof(GivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissingData))]
        public void GivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissing(IDictionary<Reference<AggregateRoot>, bool> references)
        {
            Expression<Func<Guid, bool>> predicate = id => references
                .Where(item => item.Key.Id == id)
                .Single()
                .Value;

            _ = repository
                .Setup(repo => repo.Get(It.Is(predicate)))
                .Returns<Guid>(id => new Mock<AggregateRoot>(id, AggregateRoot.DefaultVersion).Object);
            
            AggregateException exception = Assert.Throws<AggregateException>(
                () => references.Keys.Retrieve(context.Object, repository.Object));

            repository.Verify(repo => repo.Get(It.IsAny<Guid>()), Times.Exactly(references.Count));

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

        private static IEnumerable<IDictionary<Reference<AggregateRoot>, bool>> GenerateSinglesForGivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
            Reference<AggregateRoot> reference1,
            Reference<AggregateRoot> reference2,
            Reference<AggregateRoot> reference3)
        {
            yield return new Dictionary<Reference<AggregateRoot>, bool>
            {
                { reference1, false },
                { reference2, true },
                { reference3, true },
            };

            yield return new Dictionary<Reference<AggregateRoot>, bool>
            {
                { reference1, true },
                { reference2, false },
                { reference3, true },
            };

            yield return new Dictionary<Reference<AggregateRoot>, bool>
            {
                { reference1, true },
                { reference2, true },
                { reference3, false },
            };
        }

        private static IEnumerable<IDictionary<Reference<AggregateRoot>, bool>> GenerateMultiplesForGivenOneOrMoreReferencesThatDoNotExistsThenAnAggregateNotFoundExceptionIsThrownForEachThatIsMissing(
            Reference<AggregateRoot> reference1,
            Reference<AggregateRoot> reference2,
            Reference<AggregateRoot> reference3)
        {
            yield return new Dictionary<Reference<AggregateRoot>, bool>
            {
                { reference1, true },
                { reference2, false },
                { reference3, false },
            };

            yield return new Dictionary<Reference<AggregateRoot>, bool>
            {
                { reference1, false },
                { reference2, true },
                { reference3, false },
            };

            yield return new Dictionary<Reference<AggregateRoot>, bool>
            {
                { reference1, false },
                { reference2, false },
                { reference3, true },
            };
        }
    }
}