using AutoHub.API.Models.BidModels;
using FluentValidation;

namespace AutoHub.API.Validators.ModelValidators;

public class BidCreateRequestModelValidator : AbstractValidator<BidCreateRequest>
{
    public BidCreateRequestModelValidator()
    {
        RuleFor(x => x.BidValue).NotEmpty().GreaterThan(0);

        RuleFor(x => x.UserId).NotEmpty().GreaterThanOrEqualTo(0);
    }
}
