namespace MooVC.Architecture.EntityMaximumIdValueExceededExceptionTests
{
    using System;
    using MooVC.Architecture.Serialization;
    using Xunit;

    public sealed class WhenEntityMaximumIdValueExceededExceptionIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            ulong max = 1;
            Type type = GetType();
            var original = new EntityMaximumIdValueExceededException(max, type);
            EntityMaximumIdValueExceededException deserialized = original.Clone();

            Assert.NotSame(original, deserialized);
            Assert.NotSame(original.Message, deserialized.Message);
        }
    }
}