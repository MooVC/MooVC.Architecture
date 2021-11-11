namespace MooVC.Architecture.Ddd.Serialization.SerializationInfoExtensionsTests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenTryAddReferenceIsCalled
    {
        private readonly SerializationInfo info;

        public WhenTryAddReferenceIsCalled()
        {
            info = new SerializationInfo(
                typeof(WhenTryAddInternalReferenceIsCalled),
                new FormatterConverter());
        }

        [Fact]
        public void GivenAnEmptyTypedReferenceThenTheValueIsIgnored()
        {
            Reference<SerializableAggregateRoot> reference = Reference<SerializableAggregateRoot>.Empty;
            bool added = info.TryAddReference(nameof(reference), reference);
            IDictionary<string, object?> contents = info.ToDictionary();

            Assert.False(added);
            Assert.Empty(contents);
        }

        [Fact]
        public void GivenAnEmptyUnTypedReferenceThenTheValueIsAddedWithAPrefixedName()
        {
            Reference reference = Reference<SerializableAggregateRoot>.Empty;
            bool added = info.TryAddReference(nameof(reference), reference);
            IDictionary<string, object?> contents = info.ToDictionary();
            _ = contents.TryGetValue(nameof(reference), out object? actual);

            Assert.True(added);
            Assert.NotEmpty(contents);
            Assert.Equal(reference, actual);
        }

        [Fact]
        public void GivenATypedReferenceThenTheValueIsAddedWithAPrefixedName()
        {
            var aggregate = new SerializableAggregateRoot();
            var reference = aggregate.ToReference();
            bool added = info.TryAddReference(nameof(reference), reference);
            IDictionary<string, object?> contents = info.ToDictionary();
            _ = contents.TryGetValue(nameof(reference), out object? actual);

            Assert.True(added);
            Assert.NotEmpty(contents);
            Assert.Equal(reference, actual);
        }

        [Fact]
        public void GivenAUnTypedReferenceThenTheValueIsAddedWithAPrefixedName()
        {
            var aggregate = new SerializableAggregateRoot();
            Reference reference = aggregate.ToReference();
            bool added = info.TryAddReference(nameof(reference), reference);
            IDictionary<string, object?> contents = info.ToDictionary();
            _ = contents.TryGetValue(nameof(reference), out object? actual);

            Assert.True(added);
            Assert.NotEmpty(contents);
            Assert.Equal(reference, actual);
        }
    }
}