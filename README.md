# moovc.architecture

<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/master?label=master&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/master?label=Tests%20%28master%29&style=plastic" /><BR /><img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/develop?label=develop&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/develop?label=Tests%20%28develop%29&style=plastic" /><BR /><img alt="Nuget" src="https://img.shields.io/nuget/v/moovc.architecture?style=plastic" /><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/moovc.architecture?style=plastic" /><img alt="Nuget" src="https://img.shields.io/nuget/dt/moovc.architecture?style=plastic" />

The MooVC Architecture library is designed to support the rapid development of applications that adhere to the Domain Driven Design architectural style.

MooVC was originally created as a PHP based framework back in 2009, intended to support the rapid development of object-oriented web applications based on the Model-View-Controller design pattern that were to be rendered in well-formed XHTML.  It is from this that MooVC gets its name - the <b>M</b>odel-<b>o</b>bject-<b>o</b>riented-<b>V</b>iew-<b>C</b>ontroller.

While the original MooVC PHP based framework has long since been deprecated, many of the lessons learned from it have formed the basis of solutions the author has since developed.  This library, and those related to it, are all intended to support the rapid development of high quality software that addresses a variety of use-cases.

# Upcoming Release v6.0.0

## Overview

MooVC.Architecture has been upgraded to target .Net 5.0, taking advantage of the many new language features which can be found [here](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five).

## Enhancements

- Created new contextual resource files and migrated resources from centralized resource file.
- Changed MooVC.Architecture to target version 3.x of MooVC (**Breaking Change**).
- Changed MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepository so that it is no longer serializable (**Breaking Change**).
- Changed MooVC.Architecture.Ddd.Services.ConcurrentMemoryRepository so that an instance of MooVC.Serialization.ICloner can be supplied to provide object immutability guarantee (**Breaking Change**).
- Changed MooVC.Architecture.Ddd.Services.MemoryRepository so that it is no longer serializable (**Breaking Change**).
- Changed MooVC.Architecture.Ddd.Services.MemoryRepository so that an instance of MooVC.Serialization.ICloner can be supplied to provide object immutability guarantee (**Breaking Change**).