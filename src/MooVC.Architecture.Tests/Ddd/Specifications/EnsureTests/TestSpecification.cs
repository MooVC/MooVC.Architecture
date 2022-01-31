namespace MooVC.Architecture.Ddd.Specifications.EnsureTests
{
    using System;
    using System.Linq.Expressions;

    internal class TestSpecification<T>
        : Specification<T>
    {
        private readonly bool isPassing;

        public TestSpecification(bool isPassing)
        {
            this.isPassing = isPassing;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            return _ => isPassing;
        }
    }
}