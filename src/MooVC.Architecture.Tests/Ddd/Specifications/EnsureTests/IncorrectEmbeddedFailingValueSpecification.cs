namespace MooVC.Architecture.Ddd.Specifications.EnsureTests
{
    [Requirement("Does Not Exist", typeof(Resources))]
    internal sealed class IncorrectEmbeddedFailingValueSpecification
        : TestSpecification<int>
    {
        public IncorrectEmbeddedFailingValueSpecification()
            : base(false)
        {
        }
    }
}