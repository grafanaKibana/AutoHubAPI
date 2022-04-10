﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoFixture;
using AutoHub.API.Models.UserModels;
using AutoHub.API.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests
{
    public class UserValidatorTests
    {
        private readonly Fixture _fixture;
        private readonly UserRegisterRequestModelValidator _registerValidator;
        private readonly UserLoginRequestModelValidator _loginValidator;
        private readonly UserUpdateRequestModelValidator _updateValidator;

        public UserValidatorTests()
        {
            _fixture = new Fixture();
            _registerValidator = new UserRegisterRequestModelValidator();
            _loginValidator = new UserLoginRequestModelValidator();
            _updateValidator = new UserUpdateRequestModelValidator();
        }

        [Fact]
        public void RegisterUserTestValidate_ValidModel_ShouldNotHaveError()
        {
            //Arrange
            var model = new UserRegisterRequestModel
            {
                Email = "v.zelensky@gov.ua",
                FirstName = "Volodymyr",
                LastName = "Zelensky",
                Password = "adminadmin",
                PhoneNumber = "+380670000000"
            };

            //Act
            var result = _registerValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
            result.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber);
        }

        [Fact]
        public void RegisterUserTestValidate_InvalidModel_ShouldHaveError()
        {
            //Arrange
            var model = new UserRegisterRequestModel
            {
                Email = "v.zelensky",
                FirstName = "",
                LastName = "",
                Password = "admin",
                PhoneNumber = "+38067"
            };

            //Act
            var result = _registerValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
            result.ShouldHaveValidationErrorFor(x => x.Password);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
        }

        [Fact]
        public void LoginUserTestValidate_ValidModel_ShouldNotHaveError()
        {
            //Arrange
            var model = new UserLoginRequestModel
            {
                Username = "v.zelensky@gov.ua",
                Password = "adminadmin"
            };

            //Act
            var result = _loginValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Username);
            result.ShouldNotHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void LoginUserTestValidate_InvalidModel_ShouldHaveError()
        {
            //Arrange
            var model = new UserLoginRequestModel
            {
                Username = string.Empty,
                Password = "admin"
            };

            //Act
            var result = _loginValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Username);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void UpdateUserTestValidate_ValidModel_ShouldNotHaveError()
        {
           //Arrange
           var model = new UserUpdateRequestModel
           {
               Email = "v.zelensky@gov.ua",
               FirstName = "Volodymyr",
               LastName = "Zelensky",
               PhoneNumber = "+380670000000",
           };

           //Act
           var result = _updateValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
            result.ShouldNotHaveValidationErrorFor(x => x.FirstName);
            result.ShouldNotHaveValidationErrorFor(x => x.LastName);
            result.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber);
        }

        [Fact]
        public void UpdateUserTestValidate_InvalidModel_ShouldHaveError()
        {
            //Arrange
            var model = new UserUpdateRequestModel
            {
                Email = "v.zelensky",
                FirstName = "",
                LastName = "",
                PhoneNumber = "+38067",
            };

            //Act
            var result = _updateValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
            result.ShouldHaveValidationErrorFor(x => x.FirstName);
            result.ShouldHaveValidationErrorFor(x => x.LastName);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);

        }
    }
}