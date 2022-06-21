namespace MooVC.Architecture.Ddd.Specifications;

public static partial class SpecificationExtensions
{
    public static Specification<T> Not<T>(this Specification<T> specification)
    {
        return new NotSpecification<T>(specification);
    }
}