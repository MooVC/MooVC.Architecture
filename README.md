# moovc.architecture

<img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/master?label=master&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/master?label=Tests%20%28master%29&style=plastic" /><BR /><img alt="Azure DevOps builds (branch)" src="https://img.shields.io/azure-devops/build/vmartinspaul/MooVC/3/develop?label=develop&style=plastic" /><img alt="Azure DevOps tests (branch)" src="https://img.shields.io/azure-devops/tests/vmartinspaul/MooVC/3/develop?label=Tests%20%28develop%29&style=plastic" /><BR /><img alt="Nuget" src="https://img.shields.io/nuget/v/moovc.architecture?style=plastic" /><img alt="Nuget (with prereleases)" src="https://img.shields.io/nuget/vpre/moovc.architecture?style=plastic" /><img alt="Nuget" src="https://img.shields.io/nuget/dt/moovc.architecture?style=plastic" />

The MooVC Architecture library is designed to support the rapid development of applications that adhere to the Domain Driven Design architectural style.

MooVC was originally created as a PHP based framework back in 2009, intended to support the rapid development of object-oriented web applications based on the Model-View-Controller design pattern that were to be rendered in well-formed XHTML.  It is from this that MooVC gets its name - the <b>M</b>odel-<b>o</b>bject-<b>o</b>riented-<b>V</b>iew-<b>C</b>ontroller.

While the original MooVC PHP based framework has long since been deprecated, many of the lessons learned from it have formed the basis of solutions the author has since developed.  This library, and those related to it, are all intended to support the rapid development of high quality software that addresses a variety of use-cases.

# Upcoming Release v7.0.0

## Overview

Applied asynchonrous event handling and cancellation.

## Enhancements

- Changed Architecture.Ddd.Services.AggregateSavedEventHandler to an async variant named AggregateSavedAsyncEventHandler (**Breaking Change**).
- Changed Architecture.Ddd.Services.AggregateSavingAsyncEventHandler to an async variant named AggregateSavingAsyncEventHandler (**Breaking Change**).
- Changed Architecture.Ddd.Services.DomainEventsPublishedEventHandler to an async variant named DomainEventsPublishedAsyncEventHandler (**Breaking Change**).
- Changed Architecture.Ddd.Services.DomainEventsPublishingEventHandler to an async variant named DomainEventsPublishingAsyncEventHandler (**Breaking Change**).
- Changed Architecture.Ddd.Services.Repository so that it now implements IEmitDiagnostics.
- Changed Architecture.Services.Bus so that it now implements IEmitDiagnostics.
- Changed Architecture.Services.MessageInvokedEventHandler to an async variant named MessageInvokedAsyncEventHandler (**Breaking Change**).
- Changed Architecture.Services.MessageInvokingEventHandler to an async variant named MessageInvokingAsyncEventHandler (**Breaking Change**).
- Changed the invocation of the AggregatedSaved event handler of Architecture.Ddd.Services.Repository to utilize the passive implementation (**Breaking Change**).
- Changed the invocation of the Published event handler of Architecture.Ddd.Services.Bus to utilize the passive implementation (**Breaking Change**).
- Changed the invocation of the Invoked event handler of Architecture.Services.Bus to utilize the passive implementation (**Breaking Change**).