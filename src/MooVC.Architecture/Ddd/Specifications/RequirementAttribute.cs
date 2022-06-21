namespace MooVC.Architecture.Ddd.Specifications;

using System;
using System.Resources;
using static System.String;

[AttributeUsage(AttributeTargets.Class)]
public sealed class RequirementAttribute
    : Attribute
{
    private readonly Lazy<string> description;

    public RequirementAttribute(string description)
    {
        this.description = new(() => description);
    }

    public RequirementAttribute(string resourceName, Type resourceType)
    {
        description = new(() => GetDescription(resourceName, resourceType));
    }

    public string Description => description.Value;

    private static string GetDescription(string resourceName, Type resourceType)
    {
        ResourceManager manager = new(resourceType);
        string? description = manager.GetString(resourceName);

        if (description is { })
        {
            return description;
        }

        return Empty;
    }
}