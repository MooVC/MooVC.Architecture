namespace MooVC.Architecture.Ddd.Specifications
{
    public static partial class SpecificationExtensions
    {
        public static Specification<T> And<T>(this Specification<T> left, Specification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }
    }
}