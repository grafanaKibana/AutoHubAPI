using AutoHub.API.Models.CarModels;
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
            RuleFor(x => x.CarBrand).NotEmpty();
            RuleFor(x => x.CarColor).NotEmpty();
            RuleFor(x => x.CarModel).NotEmpty();
            RuleFor(x => x.Mileage).GreaterThan(0);
            RuleFor(x => x.VIN).Must(vin => vin.Length.Equals(17))
                .WithMessage("VIN length should be equal 17 letters");
            RuleFor(x => x.SellingPrice).GreaterThanOrEqualTo(y => y.CostPrice)
                .WithMessage("Selling price must be greater than CostPrice");
            RuleFor(x => x.Year).GreaterThan(1900);
        }
    }

    public class CarUpdateRequestModelValidator : AbstractValidator<CarUpdateRequestModel>
    {
        public CarUpdateRequestModelValidator()
        {
            RuleFor(x => x.CarStatusId).Must(x => Enum.IsDefined(typeof(CarStatusEnum), x))
                .WithMessage("Incorrect status ID");
            RuleFor(x => x.CostPrice).GreaterThan(0);
            RuleFor(x => x.CarBrand).NotEmpty();
            RuleFor(x => x.CarColor).NotEmpty();
            RuleFor(x => x.CarModel).NotEmpty();
            RuleFor(x => x.Mileage).GreaterThan(0);
            RuleFor(x => x.VIN).Must(vin => vin.Length.Equals(17))
                .WithMessage("VIN length should be equal 17 letters");
            RuleFor(x => x.SellingPrice).GreaterThanOrEqualTo(y => y.CostPrice)
                .WithMessage("Selling price must be greater than CostPrice");
            RuleFor(x => x.Year).GreaterThan(1900);
        }
    }
}
