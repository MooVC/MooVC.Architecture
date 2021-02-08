namespace MooVC.Architecture.Ddd.Specifications.SpecificationExtensionsTests
{
    using System;
    using System.Linq.Expressions;

    public sealed class TestableSpecification
        : Specification<int>
    {
        private readonly bool isSatisified;

        public TestableSpecification(bool isSatisified)
        {
            this.isSatisified = isSatisified;
        }

        public override Expression<Func<int, bool>> ToExpression()
        {
            return value => isSatisified;
        }
    }
}