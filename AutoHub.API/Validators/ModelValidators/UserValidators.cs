using AutoHub.API.Models.UserModels;
using AutoHub.BLL.Constants;
using FluentValidation;

namespace AutoHub.API.Validators
{
    public class UserLoginRequestModelValidator : AbstractValidator<UserLoginRequestModel>
    {
        public UserLoginRequestModelValidator()
        {
            RuleFor(x => x.Username).NotEmpty().MustNotHaveLeadingTrailingSpaces();

            RuleFor(x => x.Password).NotEmpty().MinimumLength(UserRestrictions.MinPasswordLenght);
        }
    }

    public class UserRegisterRequestModelValidator : AbstractValidator<UserRegisterRequestModel>
    {
        public UserRegisterRequestModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MustNotHaveLeadingTrailingSpaces();

            RuleFor(x => x.Password).NotEmpty().MinimumLength(UserRestrictions.MinPasswordLenght);

            RuleFor(x => x.FirstName).NotEmpty().MustNotHaveLeadingTrailingSpaces();

            RuleFor(x => x.LastName).NotEmpty().MustNotHaveLeadingTrailingSpaces();

            RuleFor(x => x.PhoneNumber).NotEmpty().PhoneNumber().MustNotHaveLeadingTrailingSpaces();
        }
    }

    public class UserUpdateRequestModelValidator : AbstractValidator<UserUpdateRequestModel>
    {
        public UserUpdateRequestModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MustNotHaveLeadingTrailingSpaces();

            RuleFor(x => x.FirstName).NotEmpty().MustNotHaveLeadingTrailingSpaces();

            RuleFor(x => x.LastName).NotEmpty().MustNotHaveLeadingTrailingSpaces();

            RuleFor(x => x.PhoneNumber).NotEmpty().PhoneNumber().MustNotHaveLeadingTrailingSpaces();
        }
    }
}