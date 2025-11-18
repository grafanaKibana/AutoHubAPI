using System;
using AutoHub.Domain.Constants;
using FluentValidation;

namespace AutoHub.API.Validators;

/// <summary>
/// Class for storing custom entities validator rules.
/// </summary>
public static class RulesExtensions
{
    /// <param name="ruleBuilder">The rule builder on which the validator should be defined.</param>
    /// <typeparam name="T">Type of entity being validated.</typeparam>
    extension<T>(IRuleBuilder<T, string> ruleBuilder)
    {
        /// <summary>
        /// Defines a 'MustNotHaveLeadingTrailingSpace' validator on the current rule builder. 
        /// The validation will succeed if the property value not contains whitespace in the start or in the end of the string. 
        /// The validation will fail if the property value contains whitespace in the start or in the end of the string.
        /// </summary>
        /// <returns>Returns <see cref="IRuleBuilderOptions{T, TElement}"/></returns>
        public IRuleBuilderOptions<T, string> MustNotHaveLeadingTrailingSpaces() =>
            ruleBuilder
                .Must(value => value is null || value.Length == value.Trim().Length)
                .WithMessage("Leading and trailing spaces are not allowed.");

        /// <summary>
        /// Defines a 'MustBeInUpperCase' validator on the current rule builder. 
        /// The validation will succeed if the property value contains all characters in upper case. 
        /// The validation will fail if the property value contains at least single character in lower case .
        /// </summary>
        /// <returns>Returns <see cref="IRuleBuilderOptions{T, TElement}"/></returns>
        public IRuleBuilderOptions<T, string> MustBeInUpperCase() =>
            ruleBuilder
                .Must(value => value is null || string.Equals(value, value.ToUpperInvariant(), StringComparison.Ordinal))
                .WithMessage("Value should be in upper case.");

        /// <summary>
        /// Defines a 'PhoneNumber' validator on the current rule builder. 
        /// The validation will succeed if the property value matches universal phone number RegEx expression. 
        /// The validation will fail if the property value not match universal phone number RegEx expression.
        /// </summary>
        /// <returns>Returns <see cref="IRuleBuilderOptions{T, TElement}"/></returns>
        public IRuleBuilderOptions<T, string> PhoneNumber() =>
            ruleBuilder
                .Matches(UserRestrictions.UniversalPhoneNumberRegex)
                .WithMessage("Value must be a phone number.");

        /// <summary>
        /// Defines a 'MustBeValidBase64String' validator on the current rule builder. 
        /// The validation will succeed if the property value is can be converted from base64 string to decoded value. 
        /// The validation will fail if the property value is cannot be converted from base64 string to decoded value.
        /// </summary>
        public IRuleBuilderOptions<T, string> MustBeValidBase64String() => 
            ruleBuilder
                .Must(value => Convert.TryFromBase64String(value, new Span<byte>(new byte[value.Length]), out int _))
                .WithMessage("The input is not a valid Base - 64 string as it contains a non - base 64 character, more than two padding characters, or an illegal character among the padding characters.");
    }
}
