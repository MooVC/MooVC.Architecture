namespace MooVC.Architecture.Ddd.ValueTests
{
    using System.Collections.Generic;

    public sealed class TestValue
        : Value
    {
        public TestValue(int a, int b)
        {
            A = a;
            B = b;
        }

        public int A { get; }

        public int B { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return A;
            yield return B;
        }
    }
}
