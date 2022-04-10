using AutoHub.API.Models.CarColorModels;
using FluentValidation;

namespace AutoHub.API.Validators
{
    public class CarColorCreateRequestModelValidator : AbstractValidator<CarColorCreateRequestModel>
    {
        public CarColorCreateRequestModelValidator()
        {
            RuleFor(x => x.CarColorName).NotEmpty().MustNotHaveLeadingTrailingSpaces();
        }
    }
    public class CarColorUpdateRequestModelValidator : AbstractValidator<CarColorUpdateRequestModel>
    {
        public CarColorUpdateRequestModelValidator()
        {
            RuleFor(x => x.CarColorName).NotEmpty().MustNotHaveLeadingTrailingSpaces();
        }
    }
}
