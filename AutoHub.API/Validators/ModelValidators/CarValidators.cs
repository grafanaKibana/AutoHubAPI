using System;
using AutoHub.API.Models.CarModels;
using AutoHub.Domain.Constants;
using AutoHub.Domain.Enums;
using FluentValidation;

namespace AutoHub.API.Validators.ModelValidators;

public class CarCreateRequestModelValidator : AbstractValidator<CarCreateRequest>
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
            .Length(CarRestrictions.VINLength)
            .WithMessage($"{nameof(CarCreateRequest.VIN)} length should be equal {CarRestrictions.VINLength} characters");

        RuleFor(x => x.SellingPrice)
            .NotEmpty()
            .GreaterThanOrEqualTo(y => y.CostPrice)
            .WithMessage($"{nameof(CarCreateRequest.SellingPrice)} must be greater than {nameof(CarCreateRequest.CostPrice)}");

        RuleFor(x => x.Year).NotEmpty().GreaterThan(CarRestrictions.MinYear);
    }
}

public class CarUpdateRequestModelValidator : AbstractValidator<CarUpdateRequest>
{
    public CarUpdateRequestModelValidator()
    {
        RuleFor(model => model.CarStatusId)
            .NotEmpty()
            .Must(id => Enum.IsDefined(typeof(CarStatusEnum), id))
            .WithMessage($"Incorrect {nameof(CarUpdateRequest.CarStatusId)} value.");

        RuleFor(x => x.CostPrice).GreaterThan(0);

        RuleFor(x => x.CarBrand).NotEmpty();

        RuleFor(x => x.CarColor).NotEmpty();

        RuleFor(x => x.CarModel).NotEmpty();

        RuleFor(x => x.Mileage).NotEmpty().GreaterThan(0);

        RuleFor(x => x.VIN)
            .NotEmpty()
            .Length(CarRestrictions.VINLength)
            .WithMessage($"{nameof(CarUpdateRequest.VIN)} length should be equal {CarRestrictions.VINLength} characters.");

        RuleFor(x => x.SellingPrice)
            .NotEmpty()
            .GreaterThanOrEqualTo(y => y.CostPrice)
            .WithMessage($"{nameof(CarUpdateRequest.SellingPrice)} must be greater than {nameof(CarUpdateRequest.CostPrice)}.");

        RuleFor(x => x.Year).NotEmpty().GreaterThan(CarRestrictions.MinYear);
    }
}
