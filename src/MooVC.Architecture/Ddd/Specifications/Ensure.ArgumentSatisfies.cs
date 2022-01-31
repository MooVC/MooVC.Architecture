namespace MooVC.Architecture.Ddd.Specifications
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using static System.String;
    using static MooVC.Architecture.Ddd.Specifications.Resources;
    using static MooVC.Ensure;

    public static partial class Ensure
    {
        public static T ArgumentSatisifies<T>(
            [NotNull] T? argument,
            string argumentName,
            Specification<T> specification)
            where T : struct
        {
            return ArgumentSatisifies(
                argument,
                argumentName,
                specification,
                Empty);
        }

        public static T ArgumentSatisifies<T>(
            [NotNull] T? argument,
            string argumentName,
            Specification<T> specification)
            where T : class
        {
            return ArgumentSatisifies(
                argument,
                argumentName,
                specification,
                Empty);
        }

        public static T ArgumentSatisifies<T>(
            [NotNull] T? argument,
            string argumentName,
            Specification<T> specification,
            string message)
            where T : struct
        {
            _ = ArgumentNotNull(
                specification,
                nameof(specification),
                EnsureArgumentSatisfiesSpecificationRequired);

            return ArgumentIsAcceptable(
                argument,
                argumentName,
                value => specification.IsSatisfiedBy(value),
                FormatMessage(specification, message: message));
        }

        public static T ArgumentSatisifies<T>(
            [NotNull] T? argument,
            string argumentName,
            Specification<T> specification,
            string message)
            where T : class
        {
            _ = ArgumentNotNull(
                specification,
                nameof(specification),
                EnsureArgumentSatisfiesSpecificationRequired);

            return ArgumentIsAcceptable(
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

            return Join(message, Environment.NewLine, Environment.NewLine, attribute.Description)
                .TrimEnd();
        }
    }
}