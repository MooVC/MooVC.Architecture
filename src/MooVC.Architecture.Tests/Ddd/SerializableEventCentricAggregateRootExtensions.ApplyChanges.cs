namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using MooVC.Architecture.MessageTests;

    internal static class SerializableEventCentricAggregateRootExtensions
    {
        public static IEnumerable<DomainEvent> ApplyChanges(
            this SerializableEventCentricAggregateRoot original,
            SerializableMessage context,
            bool commit = true,
            int times = 3)
        {
            var events = new List<DomainEvent>();

            for (int index = 0; index < times; index++)
            {
                original.Set(new SetRequest(context, Guid.NewGuid()));

                events.AddRange(original.GetUncommittedChanges());

                if (commit)
                {
                    original.MarkChangesAsCommitted();
                }
            }

            return events;
        }
    }
}