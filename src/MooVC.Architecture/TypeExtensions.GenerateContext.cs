namespace MooVC.Architecture;

using System;
using static MooVC.Architecture.Resources;
using static MooVC.Ensure;

public static partial class TypeExtensions
{
    private static string GenerateContext(this Type type)
    {
        _ = ArgumentNotNull(type, nameof(type), TypeExtensionsGenerateContextTypeRequired);

        return type.FullName ?? type.ToString();
    }
}