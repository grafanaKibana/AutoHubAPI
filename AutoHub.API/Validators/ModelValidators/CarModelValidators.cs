using AutoHub.API.Models.CarModelModels;
using FluentValidation;

namespace AutoHub.API.Validators.ModelValidators;

public class CarModelCreateRequestModelValidator : AbstractValidator<CarModelCreateRequest>
{
    public CarModelCreateRequestModelValidator()
    {
        RuleFor(x => x.CarModelName).NotEmpty().MustNotHaveLeadingTrailingSpaces();
    }
}

public class CarModelUpdateRequestModelValidator : AbstractValidator<CarModelUpdateRequest>
{
    public CarModelUpdateRequestModelValidator()
    {
        RuleFor(x => x.CarModelName).NotEmpty().MustNotHaveLeadingTrailingSpaces();
    }
}
