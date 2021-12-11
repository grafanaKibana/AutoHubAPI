using System;
using System.Data;
using AutoHub.API.Models.CarModels;
using AutoHub.DAL.Enums;
using FluentValidation;

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
            RuleFor(x => x.VIN).Must(vin => vin.Length.Equals(17));
            RuleFor(x => x.SellingPrice).GreaterThanOrEqualTo(y => y.CostPrice);
            RuleFor(x => x.Year).GreaterThan(1900);
        }
    }

    public class CarUpdateRequestModelValidator : AbstractValidator<CarUpdateRequestModel>
    {
        public CarUpdateRequestModelValidator()
        {
            RuleFor(x => x.CarStatusId).Must(x => !Enum.IsDefined(typeof(CarStatusEnum), x));
            RuleFor(x => x.CarBrand).NotEmpty();
            RuleFor(x => x.CarColor).NotEmpty();
            RuleFor(x => x.CarModel).NotEmpty();
            RuleFor(x => x.Mileage).GreaterThan(0);
            RuleFor(x => x.VIN).Must(vin => vin.Length.Equals(17));
            RuleFor(x => x.SellingPrice).GreaterThanOrEqualTo(y => y.CostPrice);
            RuleFor(x => x.Year).GreaterThan(1900);
        }
    }
}
