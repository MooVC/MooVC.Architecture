namespace MooVC.Architecture.Ddd.Specifications.EnsureTests;

using static MooVC.Architecture.Ddd.Specifications.EnsureTests.Resources;

[Requirement(nameof(EmbeddedFailingReferenceSpecificationRequirement), typeof(Resources))]
internal sealed class EmbeddedFailingReferenceSpecification
    : TestSpecification<string>
{
    public EmbeddedFailingReferenceSpecification()
        : base(false)
    {
    }
}