namespace MooVC.Architecture.Ddd;

using System;
using System.Collections.Generic;
using MooVC.Linq;
using static System.Environment;
using static System.String;
using static MooVC.Architecture.Ddd.Resources;

public sealed class AggregateInvariantsNotSatisfiedDomainException
    : DomainException
{
    public AggregateInvariantsNotSatisfiedDomainException(Request request, Reference aggregate, IEnumerable<string> explainations)
        : base(aggregate, request, FormatMessage(aggregate, explainations, request))
    {
        Explainations = explainations.ToArrayOrEmpty();
    }

    public IEnumerable<string> Explainations { get; }

    private static string FormatMessage(Reference aggregate, IEnumerable<string> explainations, Request request)
    {
        return AggregateInvariantsNotSatisfiedDomainExceptionMessage.Format(
            request.GetType(),
            aggregate.Type,
            Join(NewLine, explainations));
    }
}