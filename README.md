# moovc.architecture

<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/master?label=master&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/master?label=Tests%20%28master%29&style=plastic" /><BR /><img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/develop?label=develop&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/develop?label=Tests%20%28develop%29&style=plastic" /><BR /><img alt="Nuget" src="https://img.shields.io/nuget/v/moovc.architecture?style=plastic" /><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/moovc.architecture?style=plastic" /><img alt="Nuget" src="https://img.shields.io/nuget/dt/moovc.architecture?style=plastic" />

The MooVC Architecture library is designed to support the rapid development of applications that adhere to the Domain Driven Design architectural style.

MooVC was originally created as a PHP based framework back in 2009, intended to support the rapid development of object-oriented web applications based on the Model-View-Controller design pattern that were to be rendered in well-formed XHTML.  It is from this that MooVC gets its name - the <b>M</b>odel-<b>o</b>bject-<b>o</b>riented-<b>V</b>iew-<b>C</b>ontroller.

While the original MooVC PHP based framework has long since been deprecated, many of the lessons learned from it have formed the basis of solutions the author has since developed.  This library, and those related to it, are all intended to support the rapid development of high quality software that addresses a variety of use-cases.

# Release v10.0.0

This release focuses on adding a greater degree of flexibility into the framework, following feedback gained from years of successful implementations.  Features that have resulted in unneccessary overhead have been removed, with additional features added to support automation and reduce the overall code footprint required.

## Enhancements

- Added a new constructor variant to Architecture.Cqrs.Services.PaginatedResult<TQuery, T>, enable initialization via an instance of Linq.PagedResult<TQuery, T>.
- Added Architecture.Ddd.Collections.EnumerableExtensions.ToUnversioned extension to convert a collection of references into their unversioned equivilent.
- Added Architecture.Ddd.Collections.EnumerableExtensions.ToUnversioned<TAggregate> extension to convert a collection of typed references into their unversioned equivilent.
- Added Architecture.Ddd.MessageExtensions.TryIdentify extension to determine the contextual refernece with a message (if applicable).
- Added Architecture.Ddd.Reference<T1-5> to support situations where a reference may be required to be within a subset of types.  The classes are abstract to ensure the implementation provides meaningful accessors, rather than the internal generic accessors like 'first/second etc'.
- Added Architecture.Ddd.ReferenceExtensions.Is extension to determine if an instance of Ddd.Reference is of a specific aggregate type.
- Added Ddd.Threading.AggregateCoordinator to provide a more concise implementation to coordination in the context of a specific aggregate.
- Added Ddd.Threading.IAggregateCoordinator to provide a more concise approach to coordinating in the context of a specific aggregate.
- Changed Architecture.Ddd.Collections.EnumerableExtensions.ToReferences to accept an optional parameter named unversioned with a default value of false that will determine the versioned state of the references generated (**Breaking Change**).
- Changed Architecture.Ddd.DomainEvent<TAggregate> so that the order of construction parameters is now alphabetic (**Breaking Change**).
- Changed Architecture.Ddd.DomainException<TAggregate> so that the order of construction parameters is now alphabetic (**Breaking Change**).
- Changed Architecture.Ddd.ReferenceExtensions.ToUnversioned to allow for a null reference.
- Changed Architecture.Ddd.ReferenceExtensions.ToUnversioned<TAggregate> to allow for a null reference.
- Changed Architecture.Ddd.Services.AggregateNotFoundException<TAggregate>  so that the order of construction parameters is now alphabetic (**Breaking Change**).
- Changed Architecture.Ddd.Services.AggregateVersionNotFoundException<TAggregate>  so that the order of construction parameters is now alphabetic (**Breaking Change**).
- Changed Architecture.Ddd.Services.AtomicUnit<T> so that the order of construction parameters is now alphabetic (**Breaking Change**).
- Changed Architecture.Ddd.Services.Bus so that it now accepts an optional IDiagnosticsProxy.
- Changed Architecture.Ddd.Services.CoordinatedContextHandler<TAggregate, TCommand> so that it now requires an instance of Ddd.Threading.IAggregateCoordinator<TAggregate> as a parameter on construction (**Breaking Change**).
- Changed Architecture.Ddd.Services.CoordinatedGenerateHandler<TAggregate, TCommand>.Generate so that an aggregate is no longer required in return.
- Changed Architecture.Ddd.Services.CoordinatedGenerateHandler<TAggregate, TCommand> so that it is now possible to override the generate behavior via GenerateAsync.
- Changed Architecture.Ddd.Services.CoordinatedGenerateHandler<TAggregate, TCommand> so that it is now possible to override the save behavior via SaveAsync.
- Changed Architecture.Ddd.Services.CoordinatedGenerateHandler<TAggregate, TCommand> so that it now requires an instance of Ddd.Threading.IAggregateCoordinator<TAggregate> as a parameter on construction (**Breaking Change**).
- Changed Architecture.Ddd.Services.CoordinatedHandler<TAggregate, TCommand> so that it now requires an instance of Ddd.Threading.IAggregateCoordinator<TAggregate> as its sole parameter on construction (**Breaking Change**).
- Changed Architecture.Ddd.Services.CoordinatedHandler<TAggregate, TCommand>.ExecuteAsync so that it now obtains the coordination context from a virtual new method named IdentifyCoordinationContextAsync (**Breaking Change**).
- Changed Architecture.Ddd.Services.CoordinatedReactionHandler<TAggregate, TCommand> so that it now requires an instance of Ddd.Threading.IAggregateCoordinator<TAggregate> as a parameter on construction (**Breaking Change**).
- Changed Architecture.Ddd.Services.IProjector<TAggregate, TProjection>.ProjectAsync to include an optional context parameter (**Breaking Change**).- 
- Changed Architecture.Ddd.Services.Reconciliation.AggregateReconciler so that it now accepts an optional IDiagnosticsProxy.
- Changed Architecture.Ddd.Services.Reconciliation.DefaultAggregateReconciler so that it no longer accepts a coordination timeout (it is now up to the proxies to coordinate if required) (**Breaking Change**).
- Changed Architecture.Ddd.Services.Repository<TAggregate> so that it now accepts an optional IDiagnosticsProxy.
- Changed Architecture.Services.Bus so that it now accepts an optional IDiagnosticsProxy.
- Changed Architecture.Entity<T> to implement Threading.ICoordinatable<T> via the Id property.
- Changed Architecture.Entity<T>.ToString so that the call is now forwarded to the ToString method for the instance held by the Id property.
- Changed Architecture.Message so that constructor parameter context (of type Message) is now an optional parameter.
- Changed Architecture.Message to implement Threading.ICoordinatable<Guid> via the CorrelationId property.
- Changed Architecture.Reference<T> to implement Threading.ICoordinatable<Guid> via the Id property.
- Changed to target v7.x of MooVC (**Breaking Change**).
- Removed Architecture.Cqrs.Services.EnumerableResult<T> in favour of Architecture.Cqrs.Services.EnumerableResult<TQuery, T> (**Breaking Change**).
- Removed Architecture.Cqrs.Services.IQueryEngine.QueryAsync<TResult> in favour of Architecture.Cqrs.Services.IQueryEngine.QueryAsync<TQuery, TResult>(**Breaking Change**).
- Removed Architecture.Cqrs.Services.IQueryHandler<TResult> in favour of Architecture.Cqrs.Services.IQueryHandler<TQuery, TResult> (**Breaking Change**).
- Removed Architecture.Cqrs.Services.PaginatedQuery in favour of Architecture.Cqrs.Services.PaginatedResult<TQuery, T> (**Breaking Change**).
- Removed Architecture.Cqrs.Services.PaginatedResult<T> in favour of Architecture.Cqrs.Services.PaginatedResult<TQuery, T> (**Breaking Change**).
- Removed Architecture.Cqrs.Services.Result<T> in favour of Architecture.Cqrs.Services.Result<TQuery, T> (**Breaking Change**).
- Removed Architecture.Cqrs.Services.SynchronousQueryHandler<TResult> in favour of Architecture.Cqrs.Services.SynchronousQueryHandler<TQuery, TResult> (**Breaking Change**).
- Removed Architecture.Ddd.AggregateRootExtensions.CoordinateAsync  (**Breaking Change**).
- Removed Architecture.Ddd.ReferenceExtensions.CoordinateAsync (**Breaking Change**).
- Removed Architecture.Ddd.Services.Bus.OnDiagnosticsEmittedAsync in favour of a new protected Diagnostics property that enabled access to diagnostics emission (**Breaking Change**).
- Removed Architecture.Ddd.Services.CoordinatedContextHandler<TAggregate, TCommand>.IdentifyTarget in favour of IdentifyCoordinationContextAsync or IdentifyCoordinationContext (**Breaking Change**).
- Removed Architecture.Ddd.Services.CoordinatedContextHandler<TAggregate, TCommand>.PerformCoordinatedExecuteAsync in favor of PerformExecuteAsync (**Breaking Change**).
- Removed Architecture.Ddd.Services.Reconciliation.AggregateReconciler.OnDiagnosticsEmittedAsync in favour of a new protected Diagnostics property that enabled access to diagnostics emission (**Breaking Change**).
- Removed Architecture.Ddd.Services.Repository<TAggregate>.OnDiagnosticsEmittedAsync in favour of a new protected Diagnostics property that enabled access to diagnostics emission (**Breaking Change**).
- Removed Architecture.MessageExtensions.CoordinateAsync (**Breaking Change**).
- Removed Architecture.ObjectExtensions.CoordinateAsync (**Breaking Change**).
- Removed Architecture.Services.Bus.OnDiagnosticsEmittedAsync in favour of a new protected Diagnostics property that enabled access to diagnostics emission (**Breaking Change**).
- Removed Architecture.TypeExtensions.CoordinateAsync (**Breaking Change**).
- Renamed Architecture.Ddd.Services.CoordinatedContextHandler<TAggregate, TCommand>.PerformCoordinatedExecuteAsync to PerformExecuteAsync (**Breaking Change**).
- Renamed Architecture.Ddd.Services.CoordinatedContextHandler<TAggregate, TCommand>.PerformCoordinatedRetrieveAsync to RetrieveAsync (**Breaking Change**).
- Renamed Architecture.Ddd.Services.CoordinatedContextHandler<TAggregate, TCommand>.PerformCoordinatedSaveAsync to SaveAsync (**Breaking Change**).
- Renamed Architecture.Ddd.Services.CoordinatedGenerateHandler<TAggregate, TCommand>.PerformCoordinatedGenerate to Generate (**Breaking Change**).
- Renamed Architecture.Ddd.Services.CoordinatedGenerateHandler<TAggregate, TCommand>.PerformSupplementalActivitiesAsync to VerifyAsync (**Breaking Change**).
- Renamed Architecture.Ddd.Services.CoordinatedHandler<TAggregate, TCommand>.PerformCoordinatedExecuteAsync to PerformExecuteAsync (**Breaking Change**).
- Renamed Architecture.Ddd.Services.CoordinatedOperationHandler.PerformCoordinatedOperation to Apply (**Breaking Change**).
- Removed support for .Net Standard 2.1 (**Breaking Change**).
- Removed support for .Net 5 (**Breaking Change**).