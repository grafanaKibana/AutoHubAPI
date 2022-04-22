using AutoHub.API.Models.CarColorModels;
using FluentValidation;

namespace AutoHub.API.Validators.ModelValidators;

public class CarColorCreateRequestModelValidator : AbstractValidator<CarColorCreateRequest>
{
    public CarColorCreateRequestModelValidator()
    {
        RuleFor(x => x.CarColorName).NotEmpty().MustNotHaveLeadingTrailingSpaces();
    }
}

public class CarColorUpdateRequestModelValidator : AbstractValidator<CarColorUpdateRequest>
{
    public CarColorUpdateRequestModelValidator()
    {
        RuleFor(x => x.CarColorName).NotEmpty().MustNotHaveLeadingTrailingSpaces();
    }
}