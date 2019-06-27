namespace MooVC.Architecture.Ddd
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static partial class EntityExtensions
    {
        public static byte NextId(this IEnumerable<Entity<byte>> entities, bool reassign = false, byte start = 1)
        {
            return (byte)UnsignedNextId(entities, byte.MaxValue, reassign, entity => entity.Id, start);
        }

        public static ushort NextId(this IEnumerable<Entity<ushort>> entities, bool reassign = false, ushort start = 1)
        {
            return (ushort)UnsignedNextId(entities, ushort.MaxValue, reassign, entity => entity.Id, start);
        }

        public static uint NextId(this IEnumerable<Entity<uint>> entities, bool reassign = false, uint start = 1)
        {
            return (uint)UnsignedNextId(entities, uint.MaxValue, reassign, entity => entity.Id, start);
        }

        public static ulong NextId(this IEnumerable<Entity<ulong>> entities, bool reassign = false, ulong start = 1)
        {
            return UnsignedNextId(entities, ulong.MaxValue, reassign, entity => entity.Id, start);
        }

        public static sbyte NextId(this IEnumerable<Entity<sbyte>> entities, bool reassign = false, sbyte start = 1)
        {
            return (sbyte)SignedNextId(entities, sbyte.MaxValue, reassign, entity => entity.Id, start);
        }

        public static short NextId(this IEnumerable<Entity<short>> entities, bool reassign = false, short start = 1)
        {
            return (short)SignedNextId(entities, short.MaxValue, reassign, entity => entity.Id, start);
        }

        public static int NextId(this IEnumerable<Entity<int>> entities, bool reassign = false, int start = 1)
        {
            return (int)SignedNextId(entities, int.MaxValue, reassign, entity => entity.Id, start);
        }

        public static long NextId(this IEnumerable<Entity<long>> entities, bool reassign = false, long start = 1)
        {
            return SignedNextId(entities, long.MaxValue, reassign, entity => entity.Id, start);
        }

        public static long SignedNextId<T>(IEnumerable<T> entities, long max, bool reassign, Func<T, long> selector, long start)
        {
            long value = entities.Select(selector).DefaultIfEmpty().Max();

            if (reassign)
            {
                if (entities.LongCount() + start > max)
                {
                    throw new EntityMaximumIdValueExceededException(max, entities.First().GetType());
                }

                IEnumerable<long> range = Range(start, Math.Max(value + 1, start));

                return range.Except(entities.Select(selector)).First();
            }
            else if (value == max)
            {
                throw new EntityMaximumIdValueExceededException(max, entities.First().GetType());
            }

            return value + 1;
        }

        public static ulong UnsignedNextId<T>(IEnumerable<T> entities, ulong max, bool reassign, Func<T, ulong> selector, ulong start)
        {
            ulong value = entities.Select(selector).DefaultIfEmpty().Max();
            
            if (reassign)
            {
                if ((ulong)entities.LongCount() + start > max)
                {
                    throw new EntityMaximumIdValueExceededException(max, entities.First().GetType());
                }

                IEnumerable<ulong> range = Range(start, Math.Max(value + 1, start));

                return range.Except(entities.Select(selector)).First();
            }
            else if (value == max)
            {
                throw new EntityMaximumIdValueExceededException(max, entities.First().GetType());
            }

            return value + 1;
        }

        private static IEnumerable<ulong> Range(ulong start, ulong end)
        {
            for (ulong value = start; value <= end; value++)
            {
                yield return value;
            }
        }

        private static IEnumerable<long> Range(long start, long end)
        {
            for (long value = start; value <= end; value++)
            {
                yield return value;
            }
        }
    }
}