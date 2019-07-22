﻿namespace MooVC.Architecture.Cqrs.Services.PaginatedQueryTests
{
    using MooVC.Linq;
    using Xunit;

    public sealed class WhenPaginatedQueryIsSerialized
    {
        [Fact]
        public void GivenAnInstanceThenAllPropertiesAreSerialized()
        {
            var query = new PaginatedQuery(new Paging());
            PaginatedQuery deserialized = query.Serialize();

            Assert.Equal(query, deserialized);
            Assert.NotSame(query, deserialized);
        }
    }
}