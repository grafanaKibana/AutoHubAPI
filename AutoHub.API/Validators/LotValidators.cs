using AutoHub.API.Models.LotModels;
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
            RuleFor(x => x.DurationInDays).NotEmpty().GreaterThan(0);
        }
    }

    public class LotUpdateRequestModelValidator : AbstractValidator<LotUpdateRequestModel>
    {
        public LotUpdateRequestModelValidator()
        {
            RuleFor(x => x.DurationInDays).GreaterThan(0);
            RuleFor(x => x.LotStatusId).NotEmpty().Must(x => Enum.IsDefined(typeof(LotStatusEnum), x));
        }
    }
}
