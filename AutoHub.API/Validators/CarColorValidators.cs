using AutoHub.API.Models.CarColorModels;
using FluentValidation;

namespace AutoHub.API.Validators
{
    public class CarColorCreateRequestModelValidator : AbstractValidator<CarColorCreateRequestModel>
    {
        public CarColorCreateRequestModelValidator()
        {
            RuleFor(x => x.CarColorName).NotEmpty();
        }
    }
    public class CarColorUpdateRequestModelValidator : AbstractValidator<CarColorUpdateRequestModel>
    {
        public CarColorUpdateRequestModelValidator()
        {
            RuleFor(x => x.CarColorName).NotEmpty();
        }
    }
}
