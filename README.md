# moovc.architecture

<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/master?label=master&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/master?label=Tests%20%28master%29&style=plastic" /><BR /><img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/develop?label=develop&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/develop?label=Tests%20%28develop%29&style=plastic" /><BR /><img alt="Nuget" src="https://img.shields.io/nuget/v/moovc.architecture?style=plastic" /><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/moovc.architecture?style=plastic" /><img alt="Nuget" src="https://img.shields.io/nuget/dt/moovc.architecture?style=plastic" />

The MooVC Architecture library is designed to support the rapid development of applications that adhere to the Domain Driven Design architectural style.

MooVC was originally created as a PHP based framework back in 2009, intended to support the rapid development of object-oriented web applications based on the Model-View-Controller design pattern that were to be rendered in well-formed XHTML.  It is from this that MooVC gets its name - the <b>M</b>odel-<b>o</b>bject-<b>o</b>riented-<b>V</b>iew-<b>C</b>ontroller.

While the original MooVC PHP based framework has long since been deprecated, many of the lessons learned from it have formed the basis of solutions the author has since developed.  This library, and those related to it, are all intended to support the rapid development of high quality software that addresses a variety of use-cases.

# Release v9.0.0

## Overview

This release focuses on addressing weaknesses in the handling of aggregate references, specifically, mismatches between their types depending on how they are created.

## Enhancements

- Added a new Architecture.Cqrs.Services.EnumerableResult that can be used to encapsulate a result that contains a collection.
- Added a new Architecture.Ddd.GuidExtensions.ToReference extension that acts as a shortcut to the new type centric Architecture.Ddd.Reference.Create variant. 
- Changed Architecture.Cqrs.Services.PaginatedResult so that it now inherits from Architecture.Cqrs.Services.EnumerableResult.
- Changed Architecture.Ddd.AggregateHasUncommittedChangesException so that it now has a constructor capable of accepting an AggregateRoot instance.
- Changed Architecture.Ddd.AggregateEventSequenceUnorderedException so that it now has a constructor capable of accepting an AggregateRoot instance.
- Changed Architecture.Ddd.AggregateEventMismatchException so that it now has a constructor capable of accepting an AggregateRoot instance.
- Changed Architecture.Ddd.AggregateHistoryInvalidForStateException so that it now has a constructor capable of accepting an AggregateRoot instance.
- Changed Architecture.Ddd.Ensure.ReferenceIsOfType so that it will return as long as the type of the reference can be assigned to the generic type provided.
- Changed Architecture.Ddd.Reference so that it now has a static Create variant, capable of directly accepting an aggregate instance.
- Changed Architecture.Ddd.Reference so that it now has a static Create variant, capable of directly accepting a type instance.
- Changed Architecture.Ddd.Reference so that it now has a static Create variant, capable of accepting a genertic AggregateRoot type.
- Changed Architecture.Ddd.Reference so that it is now marked as abstract (**breaking change**).
- Changed Architecture.Ddd.Reference so that it no longer accepts an aggregate as a constructor argument (**breaking change**).
- Changed Architecture.Ddd.Reference so that it will now return a custom formatted string when ToString is invoked.
- Changed Architecture.Ddd.Reference.Create so that the type parameter now comes after the id parameter (**breaking change**).
- Changed Architecture.Ddd.Reference<TAggregate> so that its constructors are now internal only  (**breaking change**).
- Changed Architecture.Ddd.Reference<TAggregate> so that it now has a static Create variant, capable of accepting an id and optional version.
- Changed Architecture.Ddd.ReferenceExtensions.ToTyped now accepts and validates a nullable reference.
- Changed Architecture.Ddd.SignedVersion so that the custom formatted string retutned when ToString is invoked now takes account of the empty state.