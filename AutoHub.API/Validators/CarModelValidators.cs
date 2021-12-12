using AutoHub.API.Models.CarModelModels;
using FluentValidation;

namespace AutoHub.API.Validators
{
    public class CarModelCreateRequestModelValidator : AbstractValidator<CarModelCreateRequestModel>
    {
        public CarModelCreateRequestModelValidator()
        {
            RuleFor(x => x.CarModelName).NotEmpty();
        }
    }
    public class CarModelUpdateRequestModelValidator : AbstractValidator<CarModelUpdateRequestModel>
    {
        public CarModelUpdateRequestModelValidator()
        {
            RuleFor(x => x.CarModelName).NotEmpty();
        }
    }
}
