namespace MooVC.Architecture.Ddd.Specifications.EnsureTests;

using static MooVC.Architecture.Ddd.Specifications.EnsureTests.Resources;

[Requirement(nameof(EmbeddedFailingValueSpecificationRequirement), typeof(Resources))]
internal sealed class EmbeddedFailingValueSpecification
    : TestSpecification<int>
{
    public EmbeddedFailingValueSpecification()
        : base(false)
    {
    }
}