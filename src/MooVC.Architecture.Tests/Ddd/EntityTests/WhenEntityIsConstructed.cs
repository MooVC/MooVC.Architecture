namespace MooVC.Architecture.Ddd.EntityTests
{
    using System;
    using Xunit;

    public sealed class WhenEntityIsConstructed
    {
        [Fact]
        public void GivenAnIdThenTheIdIsPropagated()
        {
            var expectedId = Guid.NewGuid();
            var entity = new SerializableEntity(expectedId);

            Assert.Equal(expectedId, entity.Id);
        }
    }
}