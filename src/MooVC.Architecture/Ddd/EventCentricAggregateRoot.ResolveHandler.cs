namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;

    public partial class EventCentricAggregateRoot
    {
        public const string HandlerName = "Handle";
        private static readonly ConcurrentDictionary<Type, MethodInfo> handlers = new ConcurrentDictionary<Type, MethodInfo>();

        protected virtual Action<TEvent>? ResolveHandler<TEvent>(DomainEvent @event)
           where TEvent : DomainEvent
        {
            MethodInfo? handler = LocateHandler(@event);

            if (handler is null)
            {
                return default;
            }

            return @event => _ = handler.Invoke(this, new object[] { @event });
        }

        private static MethodInfo? GenerateHandler(Type aggregateType, Type eventType)
        {
            return aggregateType.GetMethod(
                HandlerName,
                BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                new[] { eventType },
                null);
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
}