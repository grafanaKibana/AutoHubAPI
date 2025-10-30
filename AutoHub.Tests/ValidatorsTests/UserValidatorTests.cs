using AutoHub.API.Models.UserModels;
using AutoHub.API.Validators.ModelValidators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests;

public class UserValidatorTests
{
    private readonly UserRegisterRequestModelValidator registerValidator;
    private readonly UserLoginRequestModelValidator loginValidator;
    private readonly UserUpdateRequestModelValidator updateValidator;

    public UserValidatorTests()
    {
        registerValidator = new UserRegisterRequestModelValidator();
        loginValidator = new UserLoginRequestModelValidator();
        updateValidator = new UserUpdateRequestModelValidator();
    }

    [Fact]
    public void RegisterUserTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = new UserRegisterRequest
        {
            Email = "v.zelensky@gov.ua",
            FirstName = "Volodymyr",
            LastName = "Zelensky",
            Password = "adminadmin",
            PhoneNumber = "+380670000000"
        };

        //Act
        var result = registerValidator.TestValidate(model);

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
        var model = new UserRegisterRequest
        {
            Email = "v.zelensky",
            FirstName = "",
            LastName = "",
            Password = "admin",
            PhoneNumber = "+38067"
        };

        //Act
        var result = registerValidator.TestValidate(model);

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
        var model = new UserLoginRequest
        {
            Username = "v.zelensky@gov.ua",
            Password = "adminadmin"
        };

        //Act
        var result = loginValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Username);
        result.ShouldNotHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void LoginUserTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new UserLoginRequest
        {
            Username = string.Empty,
            Password = "admin"
        };

        //Act
        var result = loginValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Username);
        result.ShouldHaveValidationErrorFor(x => x.Password);
    }

    [Fact]
    public void UpdateUserTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = new UserUpdateRequest
        {
            Email = "v.zelensky@gov.ua",
            FirstName = "Volodymyr",
            LastName = "Zelensky",
            PhoneNumber = "+380670000000",
        };

        //Act
        var result = updateValidator.TestValidate(model);

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
        var model = new UserUpdateRequest
        {
            Email = "v.zelensky",
            FirstName = "",
            LastName = "",
            PhoneNumber = "+38067",
        };

        //Act
        var result = updateValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.Email);
        result.ShouldHaveValidationErrorFor(x => x.FirstName);
        result.ShouldHaveValidationErrorFor(x => x.LastName);
        result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
    }
}
