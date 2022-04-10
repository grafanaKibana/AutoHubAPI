using AutoHub.API.Models.CarModels;
using AutoHub.BLL.Constants;
using AutoHub.DAL.Enums;
using FluentValidation;
using System;

namespace AutoHub.API.Validators
{

    public class CarCreateRequestModelValidator : AbstractValidator<CarCreateRequestModel>
    {
        public CarCreateRequestModelValidator()
        {
            RuleFor(x => x.CostPrice).GreaterThan(0);

            RuleFor(x => x.CarBrand).NotEmpty().MustNotHaveLeadingTrailingSpaces();

            RuleFor(x => x.CarColor).NotEmpty().MustNotHaveLeadingTrailingSpaces();

            RuleFor(x => x.CarModel).NotEmpty().MustNotHaveLeadingTrailingSpaces();

            RuleFor(x => x.Mileage).NotEmpty().GreaterThan(0);

            RuleFor(x => x.VIN)
                .NotEmpty()
                .Length(CarRestrictions.VINLenght)
                .WithMessage($"{nameof(CarCreateRequestModel.VIN)} length should be equal {CarRestrictions.VINLenght} characters");

            RuleFor(x => x.SellingPrice)
                .NotEmpty()
                .GreaterThanOrEqualTo(y => y.CostPrice)
                .WithMessage($"{nameof(CarCreateRequestModel.SellingPrice)} must be greater than {nameof(CarCreateRequestModel.CostPrice)}");

            RuleFor(x => x.Year).NotEmpty().GreaterThan(CarRestrictions.MinYear);
        }
    }

    public class CarUpdateRequestModelValidator : AbstractValidator<CarUpdateRequestModel>
    {
        public CarUpdateRequestModelValidator()
        {
            RuleFor(model => model.CarStatusId)
                .NotEmpty()
                .Must(id => Enum.IsDefined(typeof(CarStatusEnum), id))
                .WithMessage($"Incorrect {nameof(CarUpdateRequestModel.CarStatusId)} value.");

            RuleFor(x => x.CostPrice).GreaterThan(0);

            RuleFor(x => x.CarBrand).NotEmpty();
            
            RuleFor(x => x.CarColor).NotEmpty();
            
            RuleFor(x => x.CarModel).NotEmpty();
            
            RuleFor(x => x.Mileage).NotEmpty().GreaterThan(0);
            
            RuleFor(x => x.VIN)
                .NotEmpty()
                .Length(CarRestrictions.VINLenght)
                .WithMessage($"{nameof(CarUpdateRequestModel.VIN)} length should be equal {CarRestrictions.VINLenght} characters.");
            
            RuleFor(x => x.SellingPrice)
                .NotEmpty()
                .GreaterThanOrEqualTo(y => y.CostPrice)
                .WithMessage($"{nameof(CarUpdateRequestModel.SellingPrice)} must be greater than {nameof(CarUpdateRequestModel.CostPrice)}.");
            
            RuleFor(x => x.Year).NotEmpty().GreaterThan(CarRestrictions.MinYear);
        }
    }
}
