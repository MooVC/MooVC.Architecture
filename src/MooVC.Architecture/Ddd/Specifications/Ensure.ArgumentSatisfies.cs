namespace MooVC.Architecture.Ddd.Specifications;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using static System.Environment;
using static System.String;
using static MooVC.Architecture.Ddd.Specifications.Resources;
using static MooVC.Ensure;

public static partial class Ensure
{
    public static T ArgumentSatisifies<T>([NotNull] T? argument, string argumentName, Specification<T> specification)
        where T : struct
    {
        return ArgumentSatisifies(argument, argumentName, specification, Empty);
    }

    public static T ArgumentSatisifies<T>([NotNull] T? argument, string argumentName, Specification<T> specification)
        where T : class
    {
        return ArgumentSatisifies(argument, argumentName, specification, Empty);
    }

    public static T ArgumentSatisifies<T>([NotNull] T? argument, string argumentName, Specification<T> specification, string message)
        where T : struct
    {
        _ = IsNotNull(specification, message: EnsureArgumentSatisfiesSpecificationRequired);

        return Satisfies(
            argument,
            argumentName,
            value => specification.IsSatisfiedBy(value),
            FormatMessage(specification, message: message));
    }

    public static T ArgumentSatisifies<T>([NotNull] T? argument, string argumentName, Specification<T> specification, string message)
        where T : class
    {
        _ = IsNotNull(specification, message: EnsureArgumentSatisfiesSpecificationRequired);

        return Satisfies(
            argument,
            argumentName,
            value => specification.IsSatisfiedBy(value),
            FormatMessage(specification, message: message));
    }

    private static string FormatMessage<T>(Specification<T> specification, string? message = default)
    {
        RequirementAttribute? attribute = specification
            .GetType()
            .GetCustomAttribute<RequirementAttribute>();

        message ??= Empty;

        if (attribute is null)
        {
            return message;
        }

        if (message == Empty)
        {
            return attribute.Description;
        }

        return Join(message, NewLine, NewLine, attribute.Description).TrimEnd();
    }
}