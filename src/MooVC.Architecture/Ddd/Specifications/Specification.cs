namespace MooVC.Architecture.Ddd.Specifications
{
    using System;
    using System.Linq.Expressions;

    public abstract class Specification<T>
    {
        public static readonly Specification<T> All = new AllSpecification<T>();

        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();

            return predicate(entity);
        }

        public abstract Expression<Func<T, bool>> ToExpression();
    }
}