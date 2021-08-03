# moovc.architecture

<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/master?label=master&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/master?label=Tests%20%28master%29&style=plastic" /><BR /><img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/develop?label=develop&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/develop?label=Tests%20%28develop%29&style=plastic" /><BR /><img alt="Nuget" src="https://img.shields.io/nuget/v/moovc.architecture?style=plastic" /><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/moovc.architecture?style=plastic" /><img alt="Nuget" src="https://img.shields.io/nuget/dt/moovc.architecture?style=plastic" />

The MooVC Architecture library is designed to support the rapid development of applications that adhere to the Domain Driven Design architectural style.

MooVC was originally created as a PHP based framework back in 2009, intended to support the rapid development of object-oriented web applications based on the Model-View-Controller design pattern that were to be rendered in well-formed XHTML.  It is from this that MooVC gets its name - the <b>M</b>odel-<b>o</b>bject-<b>o</b>riented-<b>V</b>iew-<b>C</b>ontroller.

While the original MooVC PHP based framework has long since been deprecated, many of the lessons learned from it have formed the basis of solutions the author has since developed.  This library, and those related to it, are all intended to support the rapid development of high quality software that addresses a variety of use-cases.

# Release v8.0.0

## Overview

This release focuses on the standardizing the concept of serialization as defined by MooVC.

## Enhancements

- Changed to target MooVC v5.0.0 (**Breaking Change)**.
- Changed MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepository so that the ICloner is now a required argument (**Breaking Change**).
- Changed MooVC.Architecture.Ddd.Services.MemoryRepository so that the ICloner is now a required argument (**Breaking Change**).
- Changed MooVC.Architecture.Ddd.Services.SynchronousRepository so that it now requires an instance of ICloner on construction (**Breaking Change**).
- Changed MooVC.Architecture.Ddd.Services.SynchronousRepository so that the recipient of a call to PerformUpdateStore now receives a clone of the aggregate, instead of the original aggregate (**Breaking Change**).
- Changed MooVC.Architecture.Ddd.Services.UnversionedMemoryRepository so that the ICloner is now a required argument (**Breaking Change**).
- Changed MooVC.Architecture.Ddd.Services.VersionedMemoryRepository so that the ICloner is now a required argument (**Breaking Change**).

## Bug Fixes

- Fixed a bug where a GetAllAsync call to any derivations of MemoryRepository resulted in the recipient receiving a reference to the original aggregate within the store.

## End-User Impact

