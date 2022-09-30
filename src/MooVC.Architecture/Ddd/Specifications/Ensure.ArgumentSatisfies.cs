namespace MooVC.Architecture.Ddd.Specifications;

using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Environment;
using static System.String;
using static MooVC.Architecture.Ddd.Specifications.Resources;
using static MooVC.Ensure;

public static partial class Ensure
{
    public static T Satisifies<T>(
        T? argument,
        Specification<T> specification,
        [CallerArgumentExpression("argument")] string? argumentName = default,
        T? @default = default,
        string? message = default)
        where T : struct
    {
        _ = IsNotNull(specification, message: EnsureArgumentSatisfiesSpecificationRequired);

        return Satisfies(
            argument,
            value => specification.IsSatisfiedBy(value),
            argumentName: argumentName,
            @default: @default,
            message: FormatMessage(specification, message: message));
    }

    public static T Satisifies<T>(
        T? argument,
        Specification<T> specification,
        [CallerArgumentExpression("argument")] string? argumentName = default,
        T? @default = default,
        string? message = default)
        where T : class
    {
        _ = IsNotNull(specification, message: EnsureArgumentSatisfiesSpecificationRequired);

        return Satisfies(
            argument,
            value => specification.IsSatisfiedBy(value),
            argumentName: argumentName,
            @default: @default,
            message: FormatMessage(specification, message: message));
    }

    private static string? FormatMessage<T>(Specification<T> specification, string? message = default)
    {
        RequirementAttribute? attribute = specification
            .GetType()
            .GetCustomAttribute<RequirementAttribute>();

        if (attribute is null)
        {
            return message;
        }

        if (IsNullOrWhiteSpace(message))
        {
            return attribute.Description;
        }

        return Concat(message, NewLine, NewLine, attribute.Description).TrimEnd();
    }
}