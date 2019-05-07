namespace MooVC.Architecture.Ddd.Specifications
{
    public static partial class SpecificationExtensions
    {
        public static Specification<T> AndNot<T>(this Specification<T> left, Specification<T> right)
        {
            return left.And(right.Not());
        }
    }
}