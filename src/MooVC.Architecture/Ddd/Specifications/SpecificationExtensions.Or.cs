namespace MooVC.Architecture.Ddd.Specifications;

public static partial class SpecificationExtensions
{
    public static Specification<T> Or<T>(this Specification<T> left, Specification<T> right)
    {
        return new OrSpecification<T>(left, right);
    }
}