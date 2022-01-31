namespace MooVC.Architecture.Ddd.EntityExtensionsTests
{
    using System.Collections.Generic;
    using Moq;
    using Xunit;

    public sealed class WhenNextIdIsCalled
    {
        [Theory]
        [InlineData(new object[] { new byte[] { }, 1 })]
        [InlineData(new object[] { new byte[] { 0, 1, 2 }, 3 })]
        [InlineData(new object[] { new byte[] { 1, 2, 3 }, 4 })]
        [InlineData(new object[] { new byte[] { 1, 3, 5 }, 6 })]
        public void GivenASequenceOfByteKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsMaxPlusOne(IEnumerable<byte> ids, byte expectedId)
        {
            IEnumerable<Entity<byte>> entities = GenerateEntities(ids);

            byte actualId = entities.NextId();

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new ushort[] { }, 1 })]
        [InlineData(new object[] { new ushort[] { 0, 1, 2 }, 3 })]
        [InlineData(new object[] { new ushort[] { 1, 2, 3 }, 4 })]
        [InlineData(new object[] { new ushort[] { 1, 3, 5 }, 6 })]
        public void GivenASequenceOfUShortKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsMaxPlusOne(IEnumerable<ushort> ids, ushort expectedId)
        {
            IEnumerable<Entity<ushort>> entities = GenerateEntities(ids);

            ushort actualId = entities.NextId();

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new uint[] { }, 1 })]
        [InlineData(new object[] { new uint[] { 0, 1, 2 }, 3 })]
        [InlineData(new object[] { new uint[] { 1, 2, 3 }, 4 })]
        [InlineData(new object[] { new uint[] { 1, 3, 5 }, 6 })]
        public void GivenASequenceOfUIntKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsMaxPlusOne(IEnumerable<uint> ids, uint expectedId)
        {
            IEnumerable<Entity<uint>> entities = GenerateEntities(ids);

            uint actualId = entities.NextId();

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new ulong[] { }, 1 })]
        [InlineData(new object[] { new ulong[] { 0, 1, 2 }, 3 })]
        [InlineData(new object[] { new ulong[] { 1, 2, 3 }, 4 })]
        [InlineData(new object[] { new ulong[] { 1, 3, 5 }, 6 })]
        public void GivenASequenceOfULongKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsMaxPlusOne(IEnumerable<ulong> ids, ulong expectedId)
        {
            IEnumerable<Entity<ulong>> entities = GenerateEntities(ids);

            ulong actualId = entities.NextId();

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new sbyte[] { }, 1 })]
        [InlineData(new object[] { new sbyte[] { 0, 1, 2 }, 3 })]
        [InlineData(new object[] { new sbyte[] { 1, 2, 3 }, 4 })]
        [InlineData(new object[] { new sbyte[] { -5, -3, -2 }, -1 })]
        public void GivenASequenceOfSByteKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsMaxPlusOne(IEnumerable<sbyte> ids, sbyte expectedId)
        {
            IEnumerable<Entity<sbyte>> entities = GenerateEntities(ids);

            sbyte actualId = entities.NextId();

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new short[] { }, 1 })]
        [InlineData(new object[] { new short[] { 0, 1, 2 }, 3 })]
        [InlineData(new object[] { new short[] { 1, 2, 3 }, 4 })]
        [InlineData(new object[] { new short[] { -5, -3, -2 }, -1 })]
        public void GivenASequenceOfShortKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsMaxPlusOne(IEnumerable<short> ids, short expectedId)
        {
            IEnumerable<Entity<short>> entities = GenerateEntities(ids);

            short actualId = entities.NextId();

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new int[] { }, 1 })]
        [InlineData(new object[] { new int[] { 0, 1, 2 }, 3 })]
        [InlineData(new object[] { new int[] { 1, 2, 3 }, 4 })]
        [InlineData(new object[] { new int[] { -5, -3, -2 }, -1 })]
        public void GivenASequenceOfIntKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsMaxPlusOne(IEnumerable<int> ids, int expectedId)
        {
            IEnumerable<Entity<int>> entities = GenerateEntities(ids);

            int actualId = entities.NextId();

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new long[] { }, 1 })]
        [InlineData(new object[] { new long[] { 0, 1, 2 }, 3 })]
        [InlineData(new object[] { new long[] { 1, 2, 3 }, 4 })]
        [InlineData(new object[] { new long[] { -5, -3, -2 }, -1 })]
        public void GivenASequenceOfLongKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsMaxPlusOne(IEnumerable<long> ids, long expectedId)
        {
            IEnumerable<Entity<long>> entities = GenerateEntities(ids);

            long actualId = entities.NextId();

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new byte[] { }, 1 })]
        [InlineData(new object[] { new byte[] { 0, 2, 4 }, 1 })]
        [InlineData(new object[] { new byte[] { 1, 2, 5 }, 3 })]
        [InlineData(new object[] { new byte[] { 1, 3, 5 }, 2 })]
        public void GivenASequenceOfByteKeyedEntitiesWhenReassignIsTrueThenTheNextIdIsReusedWithinTheSequence(IEnumerable<byte> ids, byte expectedId)
        {
            IEnumerable<Entity<byte>> entities = GenerateEntities(ids);

            byte actualId = entities.NextId(reassign: true);

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new ushort[] { }, 1 })]
        [InlineData(new object[] { new ushort[] { 0, 2, 4 }, 1 })]
        [InlineData(new object[] { new ushort[] { 1, 2, 5 }, 3 })]
        [InlineData(new object[] { new ushort[] { 1, 3, 5 }, 2 })]
        public void GivenASequenceOfUShortKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsReusedWithinTheSequence(IEnumerable<ushort> ids, ushort expectedId)
        {
            IEnumerable<Entity<ushort>> entities = GenerateEntities(ids);

            ushort actualId = entities.NextId(reassign: true);

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new uint[] { }, 1 })]
        [InlineData(new object[] { new uint[] { 0, 2, 4 }, 1 })]
        [InlineData(new object[] { new uint[] { 1, 2, 5 }, 3 })]
        [InlineData(new object[] { new uint[] { 1, 3, 5 }, 2 })]
        public void GivenASequenceOfUIntKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsReusedWithinTheSequence(IEnumerable<uint> ids, uint expectedId)
        {
            IEnumerable<Entity<uint>> entities = GenerateEntities(ids);

            uint actualId = entities.NextId(reassign: true);

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new ulong[] { }, 1 })]
        [InlineData(new object[] { new ulong[] { 0, 2, 4 }, 1 })]
        [InlineData(new object[] { new ulong[] { 1, 2, 5 }, 3 })]
        [InlineData(new object[] { new ulong[] { 1, 3, 5 }, 2 })]
        public void GivenASequenceOfULongKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsReusedWithinTheSequence(IEnumerable<ulong> ids, ulong expectedId)
        {
            IEnumerable<Entity<ulong>> entities = GenerateEntities(ids);

            ulong actualId = entities.NextId(reassign: true);

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new sbyte[] { }, 1 })]
        [InlineData(new object[] { new sbyte[] { 0, 2, 4 }, 1 })]
        [InlineData(new object[] { new sbyte[] { 1, 2, 5 }, 3 })]
        [InlineData(new object[] { new sbyte[] { -5, -3, -2 }, 1 })]
        public void GivenASequenceOfSByteKeyedEntitiesWhenReassignIsTrueThenTheNextIdIsReusedWithinTheSequence(IEnumerable<sbyte> ids, sbyte expectedId)
        {
            IEnumerable<Entity<sbyte>> entities = GenerateEntities(ids);

            sbyte actualId = entities.NextId(reassign: true);

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new short[] { }, 1 })]
        [InlineData(new object[] { new short[] { 0, 2, 4 }, 1 })]
        [InlineData(new object[] { new short[] { 1, 2, 5 }, 3 })]
        [InlineData(new object[] { new short[] { -5, -3, -2 }, 1 })]
        public void GivenASequenceOfShortKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsReusedWithinTheSequence(IEnumerable<short> ids, short expectedId)
        {
            IEnumerable<Entity<short>> entities = GenerateEntities(ids);

            short actualId = entities.NextId(reassign: true);

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new int[] { }, 1 })]
        [InlineData(new object[] { new int[] { 0, 2, 4 }, 1 })]
        [InlineData(new object[] { new int[] { 1, 2, 5 }, 3 })]
        [InlineData(new object[] { new int[] { -5, -3, -2 }, 1 })]
        public void GivenASequenceOfIntKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsReusedWithinTheSequence(IEnumerable<int> ids, int expectedId)
        {
            IEnumerable<Entity<int>> entities = GenerateEntities(ids);

            int actualId = entities.NextId(reassign: true);

            Assert.Equal(expectedId, actualId);
        }

        [Theory]
        [InlineData(new object[] { new long[] { }, 1 })]
        [InlineData(new object[] { new long[] { 0, 2, 4 }, 1 })]
        [InlineData(new object[] { new long[] { 1, 2, 5 }, 3 })]
        [InlineData(new object[] { new long[] { -5, -3, -2 }, 1 })]
        public void GivenASequenceOfLongKeyedEntitiesWhenReassignIsFalseThenTheNextIdIsReusedWithinTheSequence(IEnumerable<long> ids, long expectedId)
        {
            IEnumerable<Entity<long>> entities = GenerateEntities(ids);

            long actualId = entities.NextId(reassign: true);

            Assert.Equal(expectedId, actualId);
        }

        [Fact]
        public void GivenASequenceOfByteKeyedEntitiesWithAnEntryAtMaxWhenReassignIsFalseThenAnEntityMaximumIdValueExceededExceptionIsThrown()
        {
            IEnumerable<Entity<byte>> entities = GenerateEntities(new[] { byte.MaxValue });

            _ = Assert.Throws<EntityMaximumIdValueExceededException>(() => entities.NextId());
        }

        [Fact]
        public void GivenASequenceOfUShortKeyedEntitiesWithAnEntryAtMaxWhenReassignIsFalseThenAnEntityMaximumIdValueExceededExceptionIsThrown()
        {
            IEnumerable<Entity<ushort>> entities = GenerateEntities(new[] { ushort.MaxValue });

            _ = Assert.Throws<EntityMaximumIdValueExceededException>(() => entities.NextId());
        }

        [Fact]
        public void GivenASequenceOfUIntKeyedEntitiesWithAnEntryAtMaxWhenReassignIsFalseThenAnEntityMaximumIdValueExceededExceptionIsThrown()
        {
            IEnumerable<Entity<uint>> entities = GenerateEntities(new[] { uint.MaxValue });

            _ = Assert.Throws<EntityMaximumIdValueExceededException>(() => entities.NextId());
        }

        [Fact]
        public void GivenASequenceOfULongKeyedEntitiesWithAnEntryAtMaxWhenReassignIsFalseThenAnEntityMaximumIdValueExceededExceptionIsThrown()
        {
            IEnumerable<Entity<ulong>> entities = GenerateEntities(new[] { ulong.MaxValue });

            _ = Assert.Throws<EntityMaximumIdValueExceededException>(() => entities.NextId());
        }

        [Fact]
        public void GivenASequenceOfSByteKeyedEntitiesWithAnEntryAtMaxWhenReassignIsFalseThenAnEntityMaximumIdValueExceededExceptionIsThrown()
        {
            IEnumerable<Entity<sbyte>> entities = GenerateEntities(new[] { sbyte.MaxValue });

            _ = Assert.Throws<EntityMaximumIdValueExceededException>(() => entities.NextId());
        }

        [Fact]
        public void GivenASequenceOfShortKeyedEntitiesWithAnEntryAtMaxWhenReassignIsFalseThenAnEntityMaximumIdValueExceededExceptionIsThrown()
        {
            IEnumerable<Entity<short>> entities = GenerateEntities(new[] { short.MaxValue });

            _ = Assert.Throws<EntityMaximumIdValueExceededException>(() => entities.NextId());
        }

        [Fact]
        public void GivenASequenceOfIntKeyedEntitiesWithAnEntryAtMaxWhenReassignIsFalseThenAnEntityMaximumIdValueExceededExceptionIsThrown()
        {
            IEnumerable<Entity<int>> entities = GenerateEntities(new[] { int.MaxValue });

            _ = Assert.Throws<EntityMaximumIdValueExceededException>(() => entities.NextId());
        }

        [Fact]
        public void GivenASequenceOfLongKeyedEntitiesWithAnEntryAtMaxWhenReassignIsFalseThenAnEntityMaximumIdValueExceededExceptionIsThrown()
        {
            IEnumerable<Entity<long>> entities = GenerateEntities(new[] { long.MaxValue });

            _ = Assert.Throws<EntityMaximumIdValueExceededException>(() => entities.NextId());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(254)]
        public void GivenASequenceOfByteKeyedEntitiesWithAnEntryAtMaxWhenReassignIsTrueThenTheStartingValueIsReturned(byte start)
        {
            IEnumerable<Entity<byte>> entities = GenerateEntities(new[] { byte.MaxValue });

            byte nextId = entities.NextId(reassign: true, start: start);

            Assert.Equal(start, nextId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(65534)]
        public void GivenASequenceOfUShortKeyedEntitiesWithAnEntryAtMaxWhenReassignIsTrueThenTheStartingValueIsReturned(ushort start)
        {
            IEnumerable<Entity<ushort>> entities = GenerateEntities(new[] { ushort.MaxValue });

            ushort nextId = entities.NextId(reassign: true, start: start);

            Assert.Equal(start, nextId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(4294967294)]
        public void GivenASequenceOfUIntKeyedEntitiesWithAnEntryAtMaxWhenReassignIsTrueThenTheStartingValueIsReturned(uint start)
        {
            IEnumerable<Entity<uint>> entities = GenerateEntities(new[] { uint.MaxValue });

            uint nextId = entities.NextId(reassign: true, start: start);

            Assert.Equal(start, nextId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(18446744073709551614)]
        public void GivenASequenceOfULongKeyedEntitiesWithAnEntryAtMaxWhenReassignIsTrueThenTheStartingValueIsReturned(ulong start)
        {
            IEnumerable<Entity<ulong>> entities = GenerateEntities(new[] { ulong.MaxValue });

            ulong nextId = entities.NextId(reassign: true, start: start);

            Assert.Equal(start, nextId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(126)]
        [InlineData(-4)]
        public void GivenASequenceOfSByteKeyedEntitiesWithAnEntryAtMaxWhenReassignIsTrueThenTheStartingValueIsReturned(sbyte start)
        {
            IEnumerable<Entity<sbyte>> entities = GenerateEntities(new[] { sbyte.MaxValue });

            sbyte nextId = entities.NextId(reassign: true, start: start);

            Assert.Equal(start, nextId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(32766)]
        [InlineData(-4)]
        public void GivenASequenceOfShortKeyedEntitiesWithAnEntryAtMaxWhenReassignIsTrueThenTheStartingValueIsReturned(short start)
        {
            IEnumerable<Entity<short>> entities = GenerateEntities(new[] { short.MaxValue });

            short nextId = entities.NextId(reassign: true, start: start);

            Assert.Equal(start, nextId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(2147483646)]
        [InlineData(-4)]
        public void GivenASequenceOfIntKeyedEntitiesWithAnEntryAtMaxWhenReassignIsTrueThenTheStartingValueIsReturned(int start)
        {
            IEnumerable<Entity<int>> entities = GenerateEntities(new[] { int.MaxValue });

            int nextId = entities.NextId(reassign: true, start: start);

            Assert.Equal(start, nextId);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(9223372036854775806)]
        [InlineData(-4)]
        public void GivenASequenceOfLongKeyedEntitiesWithAnEntryAtMaxWhenReassignIsTrueThenTheStartingValueIsReturned(long start)
        {
            IEnumerable<Entity<long>> entities = GenerateEntities(new[] { long.MaxValue });

            long nextId = entities.NextId(reassign: true, start: start);

            Assert.Equal(start, nextId);
        }

        private static IEnumerable<Entity<TKey>> GenerateEntities<TKey>(IEnumerable<TKey> ids)
            where TKey : notnull
        {
            foreach (TKey id in ids)
            {
                var entity = new Mock<Entity<TKey>>(id);

                yield return entity.Object;
            }
        }
    }
}