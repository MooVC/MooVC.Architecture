namespace MooVC.Architecture.Ddd.Specifications.EnsureTests
{
    [Requirement(Requirement)]
    internal sealed class FailingValueSpecification
        : TestSpecification<int>
    {
        public const string Requirement = "It fails anyway.";

        public FailingValueSpecification()
            : base(false)
        {
        }
    }
}