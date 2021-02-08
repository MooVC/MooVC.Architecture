namespace MooVC.Architecture
{
    using System;

    public static partial class TypeExtensions
    {
        private static string GenerateContext(this Type type)
        {
            return type.FullName ?? type.ToString();
        }
    }
}