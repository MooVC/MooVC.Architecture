namespace MooVC.Architecture.Ddd.Specifications;

using System;
using System.Linq.Expressions;

internal sealed class AllSpecification<T>
    : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return value => true;
    }
}