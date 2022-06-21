namespace MooVC.Architecture.Ddd.Specifications;

using System;
using System.Linq;
using System.Linq.Expressions;
using static MooVC.Ensure;

internal sealed class OrSpecification<T>
    : Specification<T>
{
    private readonly Specification<T> left;
    private readonly Specification<T> right;

    public OrSpecification(Specification<T> left, Specification<T> right)
    {
        this.left = ArgumentNotNull(left, nameof(left));
        this.right = ArgumentNotNull(right, nameof(right));
    }

    public override Expression<Func<T, bool>> ToExpression()
    {
        var left = this.left.ToExpression();
        var right = this.right.ToExpression();

        BinaryExpression or = Expression.OrElse(left.Body, right.Body);

        return Expression.Lambda<Func<T, bool>>(or, left.Parameters.Single());
    }
}