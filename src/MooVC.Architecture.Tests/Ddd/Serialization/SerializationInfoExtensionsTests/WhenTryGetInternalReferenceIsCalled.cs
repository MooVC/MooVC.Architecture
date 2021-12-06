namespace MooVC.Architecture.Ddd.Serialization.SerializationInfoExtensionsTests
{
    using System.Runtime.Serialization;
    using Xunit;

    public sealed class WhenTryGetInternalReferenceIsCalled
    {
        private readonly SerializationInfo info;

        public WhenTryGetInternalReferenceIsCalled()
        {
            info = new SerializationInfo(
                typeof(WhenTryAddInternalReferenceIsCalled),
                new FormatterConverter());
        }

        [Fact]
        public void GivenAnEmptyTypedReferenceThenAnEmptyReferenceIsReturned()
        {
            Reference<SerializableAggregateRoot> original = Reference<SerializableAggregateRoot>.Empty;
            _ = info.TryAddInternalReference(nameof(original), original);
            Reference<SerializableAggregateRoot> retrieved = info.TryGetInternalReference<SerializableAggregateRoot>(nameof(original));

            Assert.Equal(original, retrieved);
        }

        [Fact]
        public void GivenAnEmptyUnTypedReferenceWhenNoDefaultIsSpecifiedThenAnEmptyUnTypedReferenceOfTheSameTypeIsReturned()
        {
            Reference original = Reference<SerializableAggregateRoot>.Empty;
            _ = info.TryAddInternalReference(nameof(original), original);
            Reference retrieved = info.TryGetInternalReference(nameof(original));

            Assert.Equal(original, retrieved);
        }

        [Fact]
        public void GivenAnEmptyUnTypedReferenceWhenADefaultIsSpecifiedThenAnEmptyUnTypedReferenceOfTheSameTypeIsReturned()
        {
            Reference original = Reference<SerializableAggregateRoot>.Empty;
            _ = info.TryAddInternalReference(nameof(original), original);
            Reference retrieved = info.TryGetInternalReference(nameof(original), defaultValue: Reference<EventCentricAggregateRoot>.Empty);

            Assert.Equal(original, retrieved);
        }

        [Fact]
        public void GivenATypedReferenceThenTheReferenceIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            var original = aggregate.ToReference();
            _ = info.TryAddInternalReference(nameof(original), original);
            Reference<SerializableAggregateRoot> retrieved = info.TryGetInternalReference<SerializableAggregateRoot>(nameof(original));

            Assert.Equal(original, retrieved);
        }

        [Fact]
        public void GivenAUnTypedReferenceThenTheReferenceIsReturned()
        {
            var aggregate = new SerializableAggregateRoot();
            Reference original = aggregate.ToReference();
            _ = info.TryAddInternalReference(nameof(original), original);
            Reference retrieved = info.TryGetInternalReference(nameof(original));

            Assert.Equal(original, retrieved);
        }
    }
}