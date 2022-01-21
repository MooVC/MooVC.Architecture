﻿namespace MooVC.Architecture.Ddd.Services.Reconciliation.DefaultReconciliationOrchestratorTests
{
    using System;
    using MooVC.Architecture.Ddd.Services.Reconciliation;
    using MooVC.Persistence;
    using Moq;

    public abstract class DefaultReconciliationOrchestratorTests
    {
        protected DefaultReconciliationOrchestratorTests()
        {
            AggregateReconciler = new Mock<IAggregateReconciler>();
            EventReconciler = new Mock<IEventReconciler>();
            SequenceFactory = sequence => new EventSequence(sequence);
            SequenceStore = new Mock<IStore<EventSequence, ulong>>();
        }

        protected Mock<IAggregateReconciler> AggregateReconciler { get; }

        protected Mock<IEventReconciler> EventReconciler { get; }

        protected Func<ulong, EventSequence> SequenceFactory { get; }

        protected Mock<IStore<EventSequence, ulong>> SequenceStore { get; }
    }
}