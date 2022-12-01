namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Concurrent;
using System.Reflection;
using MooVC.Collections.Generic;

public partial class EventCentricAggregateRoot
{
    public const string HandlerName = "Handle";
    private static readonly ConcurrentDictionary<Type, MethodInfo> handlers = new();

    protected virtual Action<TEvent>? ResolveHandler<TEvent>(DomainEvent @event)
       where TEvent : DomainEvent
    {
        MethodInfo? handler = LocateHandler(@event);

        if (handler is null)
        {
            return default;
        }

        return @event => _ = handler.Invoke(this, @event.AsArray());
    }

    private static MethodInfo? GenerateHandler(Type aggregateType, Type eventType)
    {
        return aggregateType.GetMethod(
            HandlerName,
            BindingFlags.NonPublic | BindingFlags.Instance,
            default,
            eventType.AsArray(),
            default);
    }

    private static MethodInfo? LocateHandler(DomainEvent @event)
    {
        Type eventType = @event.GetType();

        if (!handlers.TryGetValue(eventType, out MethodInfo? handler))
        {
            handler = GenerateHandler(@event.Aggregate.Type, eventType);

            if (handler is { })
            {
                _ = handlers.TryAdd(eventType, handler);
            }
        }

        return handler;
    }
}