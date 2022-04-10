using AutoHub.API.Models.LotModels;
using AutoHub.BLL.Constants;
using AutoHub.DAL.Enums;
using FluentValidation;
using System;

namespace AutoHub.API.Validators
{
    public class LotCreateRequestModelValidator : AbstractValidator<LotCreateRequestModel>
    {
        public LotCreateRequestModelValidator()
        {
            RuleFor(x => x.CarId).NotEmpty().GreaterThan(0);

            RuleFor(x => x.CreatorId).NotEmpty().GreaterThan(0);

            RuleFor(x => x.DurationInDays).NotEmpty().GreaterThanOrEqualTo(LotRestrictions.MinDurationInDays);
        }
    }

    public class LotUpdateRequestModelValidator : AbstractValidator<LotUpdateRequestModel>
    {
        public LotUpdateRequestModelValidator()
        {
            RuleFor(x => x.DurationInDays).GreaterThanOrEqualTo(LotRestrictions.MinDurationInDays);

            RuleFor(x => x.LotStatusId)
                .NotEmpty()
                .Must(id => Enum.IsDefined(typeof(LotStatusEnum), id))
                .WithMessage($"Incorrect {nameof(LotUpdateRequestModel.LotStatusId)} value.");
        }
    }
}