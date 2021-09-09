namespace MooVC.Architecture.Ddd
{
    using System.Diagnostics.CodeAnalysis;

    public static partial class ReferenceExtensions
    {
        public static bool IsEmpty([NotNullWhen(false)] this Reference? reference)
        {
            return reference?.IsEmpty ?? true;
        }
    }
}