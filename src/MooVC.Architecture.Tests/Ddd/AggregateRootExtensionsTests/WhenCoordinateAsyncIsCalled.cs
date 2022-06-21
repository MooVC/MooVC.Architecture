namespace MooVC.Architecture.Ddd.AggregateRootExtensionsTests;

using System;
using System.Threading.Tasks;
using Base = MooVC.Architecture.WhenCoordinateAsyncIsCalled;

public sealed class WhenCoordinateAsyncIsCalled
    : Base
{
    private readonly SerializableAggregateRoot aggregate;

    public WhenCoordinateAsyncIsCalled()
    {
        aggregate = new SerializableAggregateRoot();
    }

    protected override Task CoordinateAsync(Func<Task> operation, TimeSpan? timeout = default)
    {
        return aggregate.CoordinateAsync(operation, timeout: timeout);
    }
}