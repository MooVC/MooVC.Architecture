﻿namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MooVC.Architecture.Ddd;
using MooVC.Collections.Generic;
using MooVC.Serialization;
using static MooVC.Architecture.Ddd.Services.Resources;
using static MooVC.Ensure;

[Serializable]
public abstract class AtomicUnit<T>
    : ISerializable
{
    private readonly Lazy<Reference> aggregate;

    protected AtomicUnit(DomainEvent @event, T id)
        : this(@event.AsEnumerable(), id)
    {
    }

    protected AtomicUnit(IEnumerable<DomainEvent> events, T id)
    {
        events = IsNotEmpty(events, message: AtomicUnitEventsRequired, predicate: value => value is { });
        _ = Satisfies(events, HasSameAggregate, message: AtomicUnitDistinctAggregateVersionRequired);
        Events = Satisfies(events, HasSameContext, message: AtomicUnitDistinctContextRequired);

        aggregate = new Lazy<Reference>(IdentifyAggregate);
        Id = id;
    }

    protected AtomicUnit(SerializationInfo info, StreamingContext context)
    {
        aggregate = new Lazy<Reference>(IdentifyAggregate);
        Events = info.GetEnumerable<DomainEvent>(nameof(Events));
        Id = info.GetValue<T>(nameof(Id));
    }

    public Reference Aggregate => aggregate.Value;

    public IEnumerable<DomainEvent> Events { get; }

    public T Id { get; }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddEnumerable(nameof(Events), Events);
        info.AddValue(nameof(Id), Id);
    }

    private static bool HasSame<TValue>(IEnumerable<DomainEvent> events, Func<DomainEvent, TValue> selector)
    {
        return events.Select(selector).Distinct().Count() == 1;
    }

    private static bool HasSameAggregate(IEnumerable<DomainEvent> events)
    {
        return HasSame(events, @event => @event.Aggregate);
    }

    private static bool HasSameCausationId(IEnumerable<DomainEvent> events)
    {
        return HasSame(events, @event => @event.CausationId);
    }

    private static bool HasSameContext(IEnumerable<DomainEvent> events)
    {
        return HasSameCausationId(events) && HasSameCorrelationId(events);
    }

    private static bool HasSameCorrelationId(IEnumerable<DomainEvent> events)
    {
        return HasSame(events, @event => @event.CorrelationId);
    }

    private Reference IdentifyAggregate()
    {
        return Events.First().Aggregate;
    }
}