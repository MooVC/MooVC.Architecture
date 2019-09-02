namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using MooVC.Collections.Generic;

    public sealed class ChangesMarkedAsCommittedEventArgs
        : EventArgs
    {
        internal ChangesMarkedAsCommittedEventArgs(IEnumerable<DomainEvent> changes)
        {
            Changes = changes.Snapshot();
        }

        public IEnumerable<DomainEvent> Changes { get; }
    }
}