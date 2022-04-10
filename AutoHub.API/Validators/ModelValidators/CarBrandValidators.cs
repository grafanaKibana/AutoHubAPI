using AutoHub.API.Models.CarBrandModels;
using FluentValidation;

namespace AutoHub.API.Validators
{
    public class CarBrandCreateRequestModelValidator : AbstractValidator<CarBrandCreateRequestModel>
    {
        public CarBrandCreateRequestModelValidator()
        {
            RuleFor(x => x.CarBrandName).NotEmpty().MustNotHaveLeadingTrailingSpaces();
        }
    }
    public class CarBrandUpdateRequestModelValidator : AbstractValidator<CarBrandUpdateRequestModel>
    {
        public CarBrandUpdateRequestModelValidator()
        {
            RuleFor(x => x.CarBrandName).NotEmpty().MustNotHaveLeadingTrailingSpaces();
        }
    }
}
