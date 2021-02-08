﻿namespace MooVC.Architecture.Ddd.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static MooVC.Architecture.Ddd.Services.Resources;
    using static MooVC.Ensure;

    public sealed class DomainEventsUnhandledEventArgs
        : DomainEventsEventArgs
    {
        public DomainEventsUnhandledEventArgs(IEnumerable<DomainEvent> events, Func<Task> handler)
            : base(events)
        {
            ArgumentNotNull(handler, nameof(handler), DomainEventsUnhandledEventArgsHandlerRequired);

            Handler = handler;
        }

        public Func<Task> Handler { get; }
    }
}