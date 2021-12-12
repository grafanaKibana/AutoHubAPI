using System;
using AutoHub.API.Models.LotModels;
using AutoHub.DAL.Enums;
using FluentValidation;

namespace AutoHub.API.Validators
{
    public class LotCreateRequestModelValidator : AbstractValidator<LotCreateRequestModel>
    {
        public LotCreateRequestModelValidator()
        {
            RuleFor(x => x.CarId).NotEmpty();
            RuleFor(x => x.CreatorId).NotEmpty();
            RuleFor(x => x.DurationInDays).GreaterThan(0);
        }
    }

    public class LotUpdateRequestModelValidator : AbstractValidator<LotUpdateRequestModel>
    {
        public LotUpdateRequestModelValidator()
        {
            RuleFor(x => x.DurationInDays).GreaterThan(0);
            RuleFor(x => x.LotStatusId).Must(x => Enum.IsDefined(typeof(LotStatusEnum), x));
        }
    }
}
