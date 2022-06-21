namespace MooVC.Architecture.Ddd.Services;

using System;
using System.Runtime.Serialization;
using static MooVC.Architecture.Ddd.Services.Resources;

[Serializable]
public sealed class DomainEventsMissingException
    : InvalidOperationException
{
    public DomainEventsMissingException()
        : base(DomainEventsMissingExceptionMessage)
    {
    }

    private DomainEventsMissingException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}