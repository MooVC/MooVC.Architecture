namespace MooVC.Architecture.Ddd.Services.Reconciliation
{
    using System;
    using static MooVC.Architecture.Ddd.Services.Reconciliation.Resources;
    using static MooVC.Ensure;

    public sealed class UnsupportedAggregateTypeDetectedEventArgs
        : EventArgs
    {
        internal UnsupportedAggregateTypeDetectedEventArgs(Type type)
        {
            ArgumentNotNull(type, nameof(type), UnsupportedAggregateTypeDetectedEventArgsTypeRequired);

            Type = type;
        }

        public Type Type { get; }
    }
}