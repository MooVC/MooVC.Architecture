namespace MooVC.Architecture.Ddd.Serialization.SerializationInfoExtensionsTests;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MooVC.Serialization;
using Xunit;

public sealed class WhenTryAddInternalReferenceIsCalled
{
    private readonly SerializationInfo info;

    public WhenTryAddInternalReferenceIsCalled()
    {
        info = new SerializationInfo(
            typeof(WhenTryAddInternalReferenceIsCalled),
            new FormatterConverter());
    }

    [Fact]
    public void GivenAnEmptyTypedReferenceThenTheValueIsIgnored()
    {
        Reference<SerializableAggregateRoot> reference = Reference<SerializableAggregateRoot>.Empty;
        bool added = info.TryAddInternalReference(nameof(reference), reference);
        IDictionary<string, object?> contents = info.ToDictionary();

        Assert.False(added);
        Assert.Empty(contents);
    }

    [Fact]
    public void GivenAnEmptyUnTypedReferenceThenTheValueIsAddedWithAPrefixedName()
    {
        const string ExpectedName = "_reference";

        Reference reference = Reference<SerializableAggregateRoot>.Empty;
        bool added = info.TryAddInternalReference(nameof(reference), reference);
        IDictionary<string, object?> contents = info.ToDictionary();
        _ = contents.TryGetValue(ExpectedName, out object? actual);

        Assert.True(added);
        Assert.NotEmpty(contents);
        Assert.Equal(reference, actual);
    }

    [Fact]
    public void GivenATypedReferenceThenTheValueIsAddedWithAPrefixedName()
    {
        const string ExpectedName = "_reference";

        var aggregate = new SerializableAggregateRoot();
        var reference = aggregate.ToReference();
        bool added = info.TryAddInternalReference(nameof(reference), reference);
        IDictionary<string, object?> contents = info.ToDictionary();
        _ = contents.TryGetValue(ExpectedName, out object? actual);

        Assert.True(added);
        Assert.NotEmpty(contents);
        Assert.Equal(reference, actual);
    }

    [Fact]
    public void GivenAUnTypedReferenceThenTheValueIsAddedWithAPrefixedName()
    {
        const string ExpectedName = "_reference";

        var aggregate = new SerializableAggregateRoot();
        Reference reference = aggregate.ToReference();
        bool added = info.TryAddInternalReference(nameof(reference), reference);
        IDictionary<string, object?> contents = info.ToDictionary();
        _ = contents.TryGetValue(ExpectedName, out object? actual);

        Assert.True(added);
        Assert.NotEmpty(contents);
        Assert.Equal(reference, actual);
    }
}