namespace MooVC.Architecture.Ddd.Specifications
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using MooVC;

    internal sealed class NotSpecification<T>
        : Specification<T>
    {
        private readonly Specification<T> specification;

        public NotSpecification(Specification<T> specification)
        {
            Ensure.ArgumentNotNull(specification, nameof(specification));

            this.specification = specification;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var specification = this.specification.ToExpression();

            UnaryExpression not = Expression.Not(specification.Body);

            return Expression.Lambda<Func<T, bool>>(not, specification.Parameters.Single());
        }
    }
}