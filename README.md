# moovc.architecture

<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/master?label=master&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/master?label=Tests%20%28master%29&style=plastic" /><BR /><img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/develop?label=develop&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/develop?label=Tests%20%28develop%29&style=plastic" /><BR /><img alt="Nuget" src="https://img.shields.io/nuget/v/moovc.architecture?style=plastic" /><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/moovc.architecture?style=plastic" /><img alt="Nuget" src="https://img.shields.io/nuget/dt/moovc.architecture?style=plastic" />

The MooVC Architecture library is designed to support the rapid development of applications that adhere to the Domain Driven Design architectural style.

MooVC was originally created as a PHP based framework back in 2009, intended to support the rapid development of object-oriented web applications based on the Model-View-Controller design pattern that were to be rendered in well-formed XHTML.  It is from this that MooVC gets its name - the <b>M</b>odel-<b>o</b>bject-<b>o</b>riented-<b>V</b>iew-<b>C</b>ontroller.

While the original MooVC PHP based framework has long since been deprecated, many of the lessons learned from it have formed the basis of solutions the author has since developed.  This library, and those related to it, are all intended to support the rapid development of high quality software that addresses a variety of use-cases.

# Upcoming Release v6.0.0

## Overview

MooVC.Architecture has been upgraded to target .Net Standard 2.1 and .Net 5.0, taking advantage of the many new language features which can be found [here](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five).

## Enhancements

- Added async variants of the various Coordinate extensions.
- Added Cqrs.Services.SynchronousQueryEngine to facilitate migration from synchronous to asynchronous implementations of Cqrs.Services.IQueryEngine.
- Added Cqrs.Services.SynchronousQueryHandler to facilitate migration from synchronous to asynchronous implementations of Cqrs.Services.IQueryHandler.
- Added Ddd.Services.SynchronousBus to facilitate migration from synchronous to asynchronous implementations of Ddd.Services.IBus and Ddd.Services.Bus.
- Added Ddd.Services.SynchronousProjector to facilitate migration from synchronous to asynchronous implementations of Ddd.Services.IProjector.
- Added Ddd.Services.Reconciliation.SynchronousAggregateReconciliationProxy to facilitate migration from synchronous to asynchronous implementations of Ddd.Services.Reconciliation.IAggregateReconciliationProxy.
- Added Ddd.Services.Reconciliation.SynchronousAggregateReconciler to facilitate migration from synchronous to asynchronous implementations of Ddd.Services.Reconciliation.IAggregateReconciler.
- Added Ddd.Services.Reconciliation.SynchronousEventReconciler to facilitate migration from synchronous to asynchronous implementations of Ddd.Services.Reconciliation.IEventReconciler.
- Added Ddd.Services.Snapshots.SynchronousSnapshotProvider to facilitate migration from synchronous to asynchronous implementations of Ddd.Services.Snapshots.ISnapshotProvider.
- Added Services.SynchronousBus to facilitate migration from synchronous to asynchronous implementations of Services.IBus and Services.Bus.
- Added Services.SynchronousHandler to facilitate migration from synchronous to asynchronous implementations of Services.IHandler.
- Changed to target version 3.x of MooVC (**Breaking Change**).
- Created new contextual resource files and migrated resources from centralized resource file.
- Changed Entity<T> so that it now implements IEquatable<Entity<T>>.
- Changed Value so that it now implements IEquatable<Value>.
- Changed constructors for Ddd.DomainException to private protected (**Breaking Change**).
- Changed constructors for Ddd.DomainEvent to private protected (**Breaking Change**).
- Changed data type for TimeStamp properties of Message, Ddd.DomainException and Ddd.Services.Reconciliation.IEventSequence to DateTimeOffset (**Breaking Change**).
- Changed all exceptions to properly implement ISerializable.
- Changed Cqrs.Services.IQueryEngine to only support async variants of each operation (**Breaking Change**).
- Changed Cqrs.Services.IQueryHandler to only support async variants of each operation (**Breaking Change**).
- Changed Ddd.Services.AggregateNotFoundException property AggregateId of type Guid to Aggregate of type Reference<TAggregate> (**Breaking Change**).
- Changed Ddd.Services.AggregateVersionNotFoundException property Aggregate of type VersionedReference to type VersionedReference<TAggregate> (**Breaking Change**).
- Changed Ddd.Services.IBus and Ddd.Services.Bus to only support async variants of each operation (**Breaking Change**).
- Changed Ddd.Services.IProjector to only support async variants of each operation (**Breaking Change**).
- Changed Ddd.Services.IRepository and its related extensions to only support async variants of each operation (**Breaking Change**).
- Changed Ddd.Services.StartSaga and Ddd.Services.StopSaga to only support async variants of each operation (**Breaking Change**).
- Changed Ddd.Services.CoordinatedGenerateHandler to only support async variants of each operation (**Breaking Change**).
- Changed Ddd.Services.CoordinatedOperationHandler to only support async variants of each operation (**Breaking Change**).
- Changed Ddd.Services.ConcurrentMemoryRepository so that it is no longer serializable (**Breaking Change**).
- Changed Ddd.Services.ConcurrentMemoryRepository so that an instance of MooVC.Serialization.ICloner can be supplied to provide object immutability guarantee (**Breaking Change**).
- Changed Ddd.Services.MemoryRepository so that it is no longer serializable (**Breaking Change**).
- Changed Ddd.Services.MemoryRepository so that an instance of MooVC.Serialization.ICloner can be supplied to provide object immutability guarantee (**Breaking Change**).
- Changed Ddd.Services.Reconciliation.IAggregateReconciliationProxy to only support async variants of each operation (**Breaking Change**).
- Changed Ddd.Services.Reconciliation.IAggregateReconciler to only support async variants of each operation (**Breaking Change**).
- Changed Ddd.Services.Reconciliation.IEventReconciler to only support async variants of each operation (**Breaking Change**).
- Changed Ddd.Services.Reconciliation.IAggregateReconciler.ReconcileAsync to accept a params array instead of an IEnumerable of Domain Events (**Breaking Change**).
- Changed Ddd.Services.Snapshots.ISnapshotProvider to only support async variants of each operation (**Breaking Change**).
- Changed Services.IBus and Services.Bus to only support async variants of each operation (**Breaking Change**).
- Changed Services.IHandler to only support async variants of each operation (**Breaking Change**).
- Changed Serialization.SerializationInfoExtensions.TryAddReference and its related variants so that they are not type specific. 
- Deleted Services.Handler and Services.HandlerExecutionFailureException (**Breaking Change**).
- Deleted unused Ddd.Services.ConcurrentMemoryRepository.PerformRead method that did not appear to serve any purpose (**Breaking Change**).
- Moved Ddd.Entity<T> to the root namespace (**Breaking Change**).
- Moved Ddd.Value to the root namespace (**Breaking Change**).

## Bug Fixes

- Changed Message to inherit from Entity<Guid>, meaning that two messages will be deemed equal if they are of the same type and have the same Id (**Breaking Change**).
- Changed Entity<T> so that it will now only deem two instances as equal if they both are of the same type (**Breaking Change**).
- Changed Value so that it will now only deem two instances as equal if they both are of the same type (**Breaking Change**).
- Changed the manner to which the type of a reference is serialized due to a serialization failure (**Breaking Change**).
- Changed the Ddd.EventCentricAggregateRoot.ApplyChange behavior to account for inconsistent behavior on failure.

# End-User Impact

- Ddd.Entity<T> Namespace (Impact: High)

Entity was moved to facilitate a new inheritance change involving Message that would facilitate a greater degree of reuse and consistency accross the framework.  This resulted in a namespace change that would result in compilation failures for any consumer that utilized Ddd.Entity<T>.  While the solution is straightforward, every reference would need to be changed from MooVC.Architecture.Ddd to MooVC.Architecture.

- Ddd.Value Namespace  (Impact: High)

Value was moved to for consistency with Entity<T>.  This resulted in a namespace change that would result in compilation failures for any consumer that utilized Ddd.Value.  While the solution is straightforward, every reference would need to be changed from MooVC.Architecture.Ddd to MooVC.Architecture.

- Ddd.DomainEvent Constructor (Impact: Medium)

This change was applied to force consumption of the types variant Ddd.DomainEvent<TAggregate> which will always result in an instance that correctly refers to the aggregate type to which it relates.

- Ddd.DomainException Constructor (Impact: Medium)

This change was applied to force consumption of the types variant Ddd.DomainException<TAggregate> which will always result in an instance that correctly refers to the aggregate type to which it relates.

- Ddd.Services.ConcurrentMemoryRepository & Ddd.Services.MemoryRepository (Impact: Medium)

Due to the addition of the ICloner to the constructor, it is now no longer possible to clone these classes. It is recommended that consumers use the GetAll method to implement serialization if required.

- Entity<T>, Message and Value Equality (Impact: Low)

It was always intended that messages and entities with the same Id be deemed equal if their Id and type where equal.  Values too should also only be deemed equal if the individual properties and its type are the same.  It is not anticipated that this have an impact due to the intent however, this may differ depending on your use-case.

- Services.Handler & Services.HandlerExecutionFailureException (Impact: Low)

These classes where seen as offering little-to-no value and have therefore been removed.  It is recommended that generic failures be captured and handled by the bus that performs execution of the command.  Validation should also be deferred to the domain layer, where appropriate derivations of Ddd.DomainException can be thrown.