namespace MooVC.Architecture.Ddd.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using MooVC;

    internal sealed class OrSpecification<T>
        : Specification<T>
    {
        private readonly Specification<T> left;
        private readonly Specification<T> right;

        public OrSpecification(Specification<T> left, Specification<T> right)
        {
            Ensure.ArgumentNotNull(left, nameof(left));
            Ensure.ArgumentNotNull(right, nameof(right));

            this.left = left;
            this.right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var left = this.left.ToExpression();
            var right = this.right.ToExpression();

            BinaryExpression or = Expression.OrElse(left.Body, right.Body);

            return Expression.Lambda<Func<T, bool>>(or, left.Parameters.Single());
        }
    }
}