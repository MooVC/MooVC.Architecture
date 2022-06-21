namespace MooVC.Architecture.Ddd.Specifications;

using System;
using System.Linq;
using System.Linq.Expressions;
using static MooVC.Architecture.Ddd.Specifications.Resources;
using static MooVC.Ensure;

internal sealed class NotSpecification<T>
    : Specification<T>
{
    private readonly Specification<T> specification;

    public NotSpecification(Specification<T> specification)
    {
        this.specification = ArgumentNotNull(specification, nameof(specification), NotSpecificationSpecificationRequired);
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var specification = this.specification.ToExpression();

        UnaryExpression not = Expression.Not(specification.Body);

        return Expression.Lambda<Func<T, bool>>(not, specification.Parameters.Single());
    }
}