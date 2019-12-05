using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Code Quality",
    "IDE0051:Remove unused private members",
    Justification = "Method is called implicitly by the EventCentricAggregateRoot.",
    Scope = "member",
    Target = "~M:MooVC.Architecture.Ddd.EventCentricAggregateRootTests.SerializableEventCentricAggregateRoot.Handle(MooVC.Architecture.Ddd.EventCentricAggregateRootTests.SerializableCreatedDomainEvent)")]

[assembly: SuppressMessage(
    "Code Quality",
    "IDE0051:Remove unused private members",
    Justification = "Method is called implicitly by the EventCentricAggregateRoot",
    Scope = "member",
    Target = "~M:MooVC.Architecture.Ddd.EventCentricAggregateRootTests.SerializableEventCentricAggregateRoot.Handle(MooVC.Architecture.Ddd.EventCentricAggregateRootTests.SerializableSetDomainEvent)")]