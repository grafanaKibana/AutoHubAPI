using AutoHub.BusinessLogic.Common;
using AutoHub.BusinessLogic.Models;
using AutoHub.Domain.Constants;
using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace AutoHub.API.Validators;

public class PaginationValidator : AbstractValidator<PaginationParameters>
{
    public PaginationValidator()
    {
        RuleFor(p => p)
            .Must(p => p.After.IsNullOrEmpty() || p.Before.IsNullOrEmpty())
            .WithMessage(p => $"Only '{nameof(p.After)}' OR '{nameof(p.Before)}' can be provided, but not both.")
            .WithName(nameof(PaginationParameters));

        When(p => p.Limit.HasValue, () =>
        {
            RuleFor(p => p.Limit)
                .GreaterThan(0)
                .WithMessage(p => $"When provided, value of '{nameof(p.Limit)}' should be greater than zero.")
                .LessThanOrEqualTo(DefaultPaginationValues.MaxLimit)
                .WithMessage(p => $"When provided, value of '{nameof(p.Limit)}' should not be greater that {DefaultPaginationValues.MaxLimit}.");
        });

        When(p => !string.IsNullOrWhiteSpace(p.After), () =>
        {
            RuleFor(p => p.After)
                .MustBeValidBase64String();

            RuleFor(p => p.After)
                .Must(x => int.TryParse(Base64Helper.Decode(x), out _))
                .When(p => Base64Helper.TryDecode(p.After, out _))
                .WithMessage(p => $"When provided, value of '{nameof(p.After)}' should be numeric.");
        });

        When(p => !string.IsNullOrWhiteSpace(p.Before), () =>
        {
            RuleFor(p => p.Before)
                .MustBeValidBase64String();

            RuleFor(p => p.Before)
                .Must(x => int.TryParse(Base64Helper.Decode(x), out _))
                .When(p => Base64Helper.TryDecode(p.Before, out _))
                .WithMessage(p => $"When provided, value of '{nameof(p.Limit)}' should be numeric.");
        });
    }
}