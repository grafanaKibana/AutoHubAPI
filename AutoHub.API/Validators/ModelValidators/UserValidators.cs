using AutoHub.API.Models.UserModels;
using AutoHub.API.Constants;
using FluentValidation;

namespace AutoHub.API.Validators;

public class UserLoginRequestModelValidator : AbstractValidator<UserLoginRequest>
{
    public UserLoginRequestModelValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MustNotHaveLeadingTrailingSpaces();

        RuleFor(x => x.Password).NotEmpty().MinimumLength(UserRestrictions.MinPasswordLenght);
    }
}

public class UserRegisterRequestModelValidator : AbstractValidator<UserRegisterRequest>
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

public class UserUpdateRequestModelValidator : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateRequestModelValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MustNotHaveLeadingTrailingSpaces();

        RuleFor(x => x.FirstName).NotEmpty().MustNotHaveLeadingTrailingSpaces();

        RuleFor(x => x.LastName).NotEmpty().MustNotHaveLeadingTrailingSpaces();

        RuleFor(x => x.PhoneNumber).NotEmpty().PhoneNumber().MustNotHaveLeadingTrailingSpaces();
    }
}
