using AutoHub.API.Models.UserModels;
using AutoHub.DAL.Enums;
using FluentValidation;
using System;

namespace AutoHub.API.Validators
{
    public class UserLoginRequestModelValidator : AbstractValidator<UserLoginRequestModel>
    {
        public UserLoginRequestModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Must(y => y.Length >= 8);

        }
    }

    public class UserRegisterRequestModelValidator : AbstractValidator<UserRegisterRequestModel>
    {
        private const string UniversalPhoneRegex =
            @"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))";
        public UserRegisterRequestModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Must(y => y.Length > 8);
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty().Matches(UniversalPhoneRegex); //Most universal phone regex
        }
    }

    public class UserUpdateRequestModelValidator : AbstractValidator<UserUpdateRequestModel>
    {
        private const string UniversalPhoneRegex =
            @"((?:\+|00)[17](?: |\-)?|(?:\+|00)[1-9]\d{0,2}(?: |\-)?|(?:\+|00)1\-\d{3}(?: |\-)?)?(0\d|\([0-9]{3}\)|[1-9]{0,3})(?:((?: |\-)[0-9]{2}){4}|((?:[0-9]{2}){4})|((?: |\-)[0-9]{3}(?: |\-)[0-9]{4})|([0-9]{7}))";

        public UserUpdateRequestModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().Must(y => y.Length > 8);
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty().Matches(UniversalPhoneRegex); //Most universal phone regex
            RuleFor(x => x.UserRoleId).NotEmpty().Must(x => Enum.IsDefined(typeof(UserRoleEnum), x));

        }
    }
}
