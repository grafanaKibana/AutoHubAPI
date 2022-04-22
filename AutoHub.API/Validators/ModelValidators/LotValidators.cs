using AutoHub.API.Models.LotModels;
using AutoHub.Domain.Constants;
using AutoHub.Domain.Enums;
using FluentValidation;
using System;

namespace AutoHub.API.Validators.ModelValidators;

public class LotCreateRequestModelValidator : AbstractValidator<LotCreateRequest>
{
    public LotCreateRequestModelValidator()
    {
        RuleFor(x => x.CarId).NotEmpty().GreaterThan(0);

        RuleFor(x => x.CreatorId).NotEmpty().GreaterThan(0);

        RuleFor(x => x.DurationInDays).NotEmpty().GreaterThanOrEqualTo(LotRestrictions.MinDurationInDays);
    }
}

public class LotUpdateRequestModelValidator : AbstractValidator<LotUpdateRequest>
{
    public LotUpdateRequestModelValidator()
    {
        RuleFor(x => x.DurationInDays).GreaterThanOrEqualTo(LotRestrictions.MinDurationInDays);

        RuleFor(x => x.LotStatusId)
            .NotEmpty()
            .Must(id => Enum.IsDefined(typeof(LotStatusEnum), id))
            .WithMessage($"Incorrect {nameof(LotUpdateRequest.LotStatusId)} value.");
    }
}