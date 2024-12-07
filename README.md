# moovc.architecture

<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/master?label=master&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/master?label=Tests%20%28master%29&style=plastic" /><BR /><img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/develop?label=develop&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/develop?label=Tests%20%28develop%29&style=plastic" /><BR /><img alt="Nuget" src="https://img.shields.io/nuget/v/moovc.architecture?style=plastic" /><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/moovc.architecture?style=plastic" /><img alt="Nuget" src="https://img.shields.io/nuget/dt/moovc.architecture?style=plastic" />

The MooVC Architecture library is designed to support the rapid development of applications that adhere to the Domain Driven Design architectural style.

MooVC was originally created as a PHP based framework back in 2009, intended to support the rapid development of object-oriented web applications based on the Model-View-Controller design pattern that were to be rendered in well-formed XHTML.  It is from this that MooVC gets its name - the <b>M</b>odel-<b>o</b>bject-<b>o</b>riented-<b>V</b>iew-<b>C</b>ontroller.

While the original MooVC PHP based framework has long since been deprecated, many of the lessons learned from it have formed the basis of solutions the author has since developed.  This library, and those related to it, are all intended to support the rapid development of high quality software that addresses a variety of use-cases.

# Release v11.0.0

This release focuses on the removal of functionality that has been rendered obsolete by advancements within the .NET Framework. 

## Enhancements

- Added a dedicated Cqrs.Trace type to address functionality associated with distributed tracing.
- Added Cqrs.Services.IMediator to encapsulate request-response interactions within a system.
- Added Cqrs.Services.IHandler<T> to encapsulate a handler for a message that yields no response.
- Added Cqrs.Services.IHandler<T,TResult> to encapsulate a handler for a message that yields a specific response.
- Changed methods accepting a CancellationToken so that it is no longer an optional parameter (**Breaking Change**).
- Changed Ddd.AggregateRoot so that it no longer derives from Entity<Guid> (**Breaking Change**).
- Moved Entity<T>, and all supporting elements, to Ddd.Entity<T> (**Breaking Change**).
- Moved Message with Cqrs.Message (**Breaking Change**).
- Renamed Ddd.SignedVersion to Sequence (**Breaking Change**).
- Removed Cqrs.Services.EnumerableResult (**Breaking Change**).
- Removed Cqrs.Services.IQueryEngine and all related classes (**Breaking Change**).
- Removed Cqrs.Services.IQueryHandler and all related classes (**Breaking Change**).
- Removed Cqrs.Services.PaginatedQuery (**Breaking Change**).
- Removed Cqrs.Services.PaginatedResult (**Breaking Change**).
- Removed Cqrs.Services.Result (**Breaking Change**).
- Removed Ddd.Reference<T1-5> variants in favour of discriminated unions (**Breaking Change**).
- Removed ImplicitValue in favour of the .NET record type (**Breaking Change**).
- Removed Services.IBus and all related classes in favour of Cqrs.Services.IMediator (**Breaking Change**).
- Removed Services.IHandler and all related classes in favour of Cqrs.Services.IHandler (**Breaking Change**).
- Removed support for legacy serialization (**Breaking Change**).
- Removed Value in favour of the .NET record type (**Breaking Change**).