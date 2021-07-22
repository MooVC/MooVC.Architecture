namespace MooVC.Architecture.Ddd.Services.DomainEventsMissingExceptionTests
{
    using MooVC.Serialization;
    using Xunit;

    public sealed class WhenDomainEventsMissingExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenTheInstanceIsSerialized()
        {
            var original = new DomainEventsMissingException();
            DomainEventsMissingException clone = original.Clone();

            Assert.NotSame(clone, original);
        }
    }
}