namespace MooVC.Architecture.Ddd
{
    public static partial class ReferenceExtensions
    {
        public static bool IsEmpty(this Reference reference)
        {
            return reference?.IsEmpty ?? true;
        }
    }
}