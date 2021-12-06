namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using MooVC.Architecture.Ddd;
    using MooVC.Serialization;
    using static MooVC.Architecture.Ddd.Services.Resources;
    using static MooVC.Ensure;

    [Serializable]
    public abstract class AtomicUnit<T>
        : ISerializable
    {
        private readonly Lazy<Reference> aggregate;

        protected AtomicUnit(T id, DomainEvent @event)
            : this(id, new[] { @event })
        {
        }

        protected AtomicUnit(T id, IEnumerable<DomainEvent> events)
        {
            Events = ArgumentNotEmpty(
                events,
                nameof(events),
                AtomicUnitEventsRequired,
                predicate: value => value is { });

            _ = ArgumentIsAcceptable(
                Events,
                nameof(events),
                HasSameAggregate,
                AtomicUnitDistinctAggregateVersionRequired);

            _ = ArgumentIsAcceptable(
                Events,
                nameof(events),
                HasSameContext,
                AtomicUnitDistinctContextRequired);

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
}