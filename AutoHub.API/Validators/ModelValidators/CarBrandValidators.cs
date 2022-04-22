using AutoHub.API.Models.CarBrandModels;
using FluentValidation;

namespace AutoHub.API.Validators.ModelValidators;

public class CarBrandCreateRequestModelValidator : AbstractValidator<CarBrandCreateRequest>
{
    public CarBrandCreateRequestModelValidator()
    {
        RuleFor(x => x.CarBrandName).NotEmpty().MustNotHaveLeadingTrailingSpaces();
    }
}

public class CarBrandUpdateRequestModelValidator : AbstractValidator<CarBrandUpdateRequest>
{
    public CarBrandUpdateRequestModelValidator()
    {
        RuleFor(x => x.CarBrandName).NotEmpty().MustNotHaveLeadingTrailingSpaces();
    }
}