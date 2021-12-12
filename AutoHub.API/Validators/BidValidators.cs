using AutoHub.API.Models.BidModels;
using FluentValidation;

namespace AutoHub.API.Validators
{
    public class BidCreateRequestModelValidator : AbstractValidator<BidCreateRequestModel>
    {
        public BidCreateRequestModelValidator()
        {
            RuleFor(x => x.BidValue).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
