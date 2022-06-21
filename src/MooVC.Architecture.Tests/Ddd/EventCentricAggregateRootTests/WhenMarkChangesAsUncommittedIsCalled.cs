namespace MooVC.Architecture.Ddd.EventCentricAggregateRootTests;

using System;
using Xunit;

public sealed class WhenMarkChangesAsUncommittedIsCalled
{
    [Fact]
    public void WhenInvokedThenAnInvalidOperationExceptionIsThrown()
    {
        var aggregate = new SerializableEventCentricAggregateRoot();

        _ = Assert.Throws<InvalidOperationException>(
            () => aggregate.TriggerMarkChangesAsUncommitted());
    }
}