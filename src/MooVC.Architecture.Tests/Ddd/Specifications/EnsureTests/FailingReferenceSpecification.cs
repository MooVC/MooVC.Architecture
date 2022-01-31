namespace MooVC.Architecture.Ddd.Specifications.EnsureTests
{
    [Requirement(Requirement)]
    internal sealed class FailingReferenceSpecification
        : TestSpecification<string>
    {
        public const string Requirement = "It fails anyway.";

        public FailingReferenceSpecification()
            : base(false)
        {
        }
    }
}