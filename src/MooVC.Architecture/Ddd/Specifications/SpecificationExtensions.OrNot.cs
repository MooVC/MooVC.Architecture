namespace MooVC.Architecture.Ddd.Specifications
{
    public static partial class SpecificationExtensions
    {
        public static Specification<T> OrNot<T>(this Specification<T> left, Specification<T> right)
        {
            return left.Or(right.Not());
        }
    }
}