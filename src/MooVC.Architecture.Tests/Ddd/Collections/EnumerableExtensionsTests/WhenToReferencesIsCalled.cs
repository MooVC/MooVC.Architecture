namespace MooVC.Architecture.Ddd.Collections.EnumerableExtensionsTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public sealed class WhenToReferencesIsCalled
{
    [Fact]
    public void GivenAggregatesThenAnEnumerableOfTheirReferencesIsReturned()
    {
        var first = new SerializableAggregateRoot();
        var second = new SerializableAggregateRoot();
        IEnumerable<SerializableAggregateRoot> aggregates = new[] { first, second };

        IEnumerable<Reference<SerializableAggregateRoot>> references = aggregates.ToReferences();

        Assert.Equal(aggregates.Count(), references.Count());
        Assert.True(aggregates.First() == references.First());
        Assert.True(aggregates.Last() == references.Last());
    }

    [Fact]
    public void GivenNoAggregatesThenAnEmptyEnumerableIsReturned()
    {
        IEnumerable<SerializableAggregateRoot> aggregates = Enumerable.Empty<SerializableAggregateRoot>();

        IEnumerable<Reference<SerializableAggregateRoot>> references = aggregates.ToReferences();

        Assert.Empty(references);
    }

    [Fact]
    public void GivenNullAggregatesThenAnEmptyEnumerableIsReturned()
    {
        IEnumerable<SerializableAggregateRoot>? aggregates = default;

        IEnumerable<Reference<SerializableAggregateRoot>> references = aggregates.ToReferences();

        Assert.Empty(references);
    }
}