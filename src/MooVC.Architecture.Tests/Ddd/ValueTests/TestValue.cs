namespace MooVC.Architecture.Ddd.ValueTests
{
    using System.Collections.Generic;

    public sealed class TestValue
        : Value
    {
        public TestValue(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        public int A { get; }

        public int B { get; }

        public int C { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return A;
            yield return B;
            yield return C;
        }
    }
}
