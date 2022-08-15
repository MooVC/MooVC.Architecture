# moovc.architecture

<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/master?label=master&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/master?label=Tests%20%28master%29&style=plastic" /><BR /><img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/develop?label=develop&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/develop?label=Tests%20%28develop%29&style=plastic" /><BR /><img alt="Nuget" src="https://img.shields.io/nuget/v/moovc.architecture?style=plastic" /><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/moovc.architecture?style=plastic" /><img alt="Nuget" src="https://img.shields.io/nuget/dt/moovc.architecture?style=plastic" />

The MooVC Architecture library is designed to support the rapid development of applications that adhere to the Domain Driven Design architectural style.

MooVC was originally created as a PHP based framework back in 2009, intended to support the rapid development of object-oriented web applications based on the Model-View-Controller design pattern that were to be rendered in well-formed XHTML.  It is from this that MooVC gets its name - the <b>M</b>odel-<b>o</b>bject-<b>o</b>riented-<b>V</b>iew-<b>C</b>ontroller.

While the original MooVC PHP based framework has long since been deprecated, many of the lessons learned from it have formed the basis of solutions the author has since developed.  This library, and those related to it, are all intended to support the rapid development of high quality software that addresses a variety of use-cases.

# Release v10.0.0

## Enhancements

- Added a new constructor variant to Architecture.Cqrs..Services.PaginatedResult<TQuery, T>, enable initialization via an instance of Linq.PagedResult<TQuery, T>.
- Changed Architecture.Message so that constructor parameter context (of type Message) is now an optional parameter.
- Changed Architecture.Ddd.Collections.EnumerableExtensions.ToReferences to accept an optional parameter named unversioned with a default value of false that will determine the versioned state of the references generated (**Breaking Change**).
- Removed Architecture.Cqrs.Services.EnumerableResult<T> in favour of Architecture.Cqrs.Services.EnumerableResult<TQuery, T> (**Breaking Change**).
- Removed Architecture.Cqrs.Services.IQueryEngine.QueryAsync<TResult> in favour of Architecture.Cqrs.Services.IQueryEngine.QueryAsync<TQuery, TResult>(**Breaking Change**).
- Removed Architecture.Cqrs.Services.PaginatedQuery in favour of Architecture.Cqrs.Services.PaginatedResult<TQuery, T> (**Breaking Change**).
- Removed Architecture.Cqrs.Services.PaginatedResult<T> in favour of Architecture.Cqrs.Services.PaginatedResult<TQuery, T> (**Breaking Change**).
- Removed Architecture.Cqrs.Services.Result<T> in favour of Architecture.Cqrs.Services.Result<TQuery, T> (**Breaking Change**).
- Removed Architecture.Cqrs.Services.IQueryHandler<TResult> in favour of Architecture.Cqrs.Services.IQueryHandler<TQuery, TResult> (**Breaking Change**).
- Removed Architecture.Cqrs.Services.SynchronousQueryHandler<TResult> in favour of Architecture.Cqrs.Services.SynchronousQueryHandler<TQuery, TResult> (**Breaking Change**).
- Changed to target v7.x of MooVC (**Breaking Change**).