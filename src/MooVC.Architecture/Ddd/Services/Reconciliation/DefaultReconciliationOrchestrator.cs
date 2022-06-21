namespace MooVC.Architecture.Ddd.Services.Reconciliation;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MooVC.Architecture.Ddd.Services.Snapshots;
using MooVC.Persistence;
using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
using static MooVC.Ensure;

public sealed class DefaultReconciliationOrchestrator<TEventSequence>
    : ReconciliationOrchestrator
    where TEventSequence : class, IEventSequence
{
    private readonly IAggregateReconciler aggregateReconciler;
    private readonly Func<Reference, EventCentricAggregateRoot> aggregateSource;
    private readonly IEventReconciler eventReconciler;
    private readonly Func<ulong, TEventSequence> sequenceFactory;
    private readonly IStore<TEventSequence, ulong> sequenceStore;
    private readonly Func<ISnapshot> snapshotSource;

    public DefaultReconciliationOrchestrator(
        IAggregateReconciler aggregateReconciler,
        Func<Reference, EventCentricAggregateRoot> aggregateSource,
        IEventReconciler eventReconciler,
        Func<ulong, TEventSequence> sequenceFactory,
        IStore<TEventSequence, ulong> sequenceStore,
        Func<ISnapshot> snapshotSource)
    {
        this.aggregateReconciler = ArgumentNotNull(
            aggregateReconciler,
            nameof(aggregateReconciler),
            DefaultReconciliationOrchestratorAggregateReconcilerRequired);

        this.aggregateSource = ArgumentNotNull(
            aggregateSource,
            nameof(aggregateSource),
            DefaultReconciliationOrchestratorAggregateSourceRequired);

        this.eventReconciler = ArgumentNotNull(
            eventReconciler,
            nameof(eventReconciler),
            DefaultReconciliationOrchestratorEventReconcilerRequired);

        this.sequenceFactory = ArgumentNotNull(
            sequenceFactory,
            nameof(sequenceFactory),
            DefaultReconciliationOrchestratorSequenceFactoryRequired);

        this.sequenceStore = ArgumentNotNull(
            sequenceStore,
            nameof(sequenceStore),
            DefaultReconciliationOrchestratorSequenceStoreRequired);

        this.snapshotSource = ArgumentNotNull(
            snapshotSource,
            nameof(snapshotSource),
            DefaultReconciliationOrchestratorSnapshotSourceRequired);

        this.aggregateReconciler.AggregateConflictDetected += AggregateReconciler_AggregateConflictDetected;
        this.eventReconciler.EventSequenceAdvanced += EventReconciler_EventSequenceAdvanced;
    }

    public override async Task ReconcileAsync(CancellationToken? cancellationToken = default, IEventSequence? target = default)
    {
        IEventSequence? previous = await GetPreviousSequenceAsync(cancellationToken)
            .ConfigureAwait(false);

        if (previous is null || previous.Sequence == 0)
        {
            previous = await RestoreLatestSnapshotAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        ReconcileEvents(previous, target);
    }

    private async Task<IEventSequence?> GetPreviousSequenceAsync(CancellationToken? cancellationToken)
    {
        IEnumerable<TEventSequence>? last = await sequenceStore
            .GetAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return last.LastOrDefault();
    }

    private void ReconcileEvents(IEventSequence? previous, IEventSequence? target)
    {
        _ = eventReconciler.ReconcileAsync(previous: previous?.Sequence, target: target?.Sequence);
    }

    private async Task<IEventSequence?> RestoreLatestSnapshotAsync(CancellationToken? cancellationToken)
    {
        ISnapshot latest = snapshotSource();

        if (latest is { })
        {
            await OnSnapshotRestorationCommencingAsync(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            await aggregateReconciler
                .ReconcileAsync(latest.Aggregates, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            await UpdateSequenceAsync(latest.Sequence.Sequence, cancellationToken)
                .ConfigureAwait(false);

            await OnSnapshotRestorationCompletedAsync(latest.Sequence, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return latest.Sequence;
        }

        return default;
    }

    private async Task UpdateSequenceAsync(ulong current, CancellationToken? cancellationToken)
    {
        TEventSequence sequence = sequenceFactory(current);

        if (sequence is { })
        {
            _ = await sequenceStore
                .CreateAsync(sequence, cancellationToken)
                .ConfigureAwait(false);
        }
    }

    private async Task AggregateReconciler_AggregateConflictDetected(IAggregateReconciler sender, AggregateConflictDetectedAsyncEventArgs e)
    {
        EventCentricAggregateRoot aggregate = aggregateSource(e.Aggregate);

        await sender
            .ReconcileAsync(aggregate)
            .ConfigureAwait(false);
    }

    private async Task EventReconciler_EventSequenceAdvanced(IEventReconciler sender, EventSequenceAdvancedAsyncEventArgs e)
    {
        await UpdateSequenceAsync(e.Sequence, e.CancellationToken)
            .ConfigureAwait(false);
    }
}