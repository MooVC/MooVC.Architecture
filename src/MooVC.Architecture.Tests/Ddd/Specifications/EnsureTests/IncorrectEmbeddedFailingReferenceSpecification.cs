namespace MooVC.Architecture.Ddd.Specifications.EnsureTests;

[Requirement("Does Not Exist", typeof(Resources))]
internal sealed class IncorrectEmbeddedFailingReferenceSpecification
    : TestSpecification<string>
{
    public IncorrectEmbeddedFailingReferenceSpecification()
        : base(false)
    {
    }
}