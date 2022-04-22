using AutoHub.API.Models.BidModels;
using AutoHub.API.Validators.ModelValidators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests;

public class BidValidatorTests
{
    private readonly BidCreateRequestModelValidator _createValidator;

    public BidValidatorTests()
    {
        _createValidator = new BidCreateRequestModelValidator();
    }

    [Fact]
    public void CreateBidTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = new BidCreateRequest
        {
            BidValue = 25500,
            UserId = 1
        };

        //Act
        var result = _createValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.BidValue);
        result.ShouldNotHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void CreateBidTestValidate_InvalidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = new BidCreateRequest
        {
            BidValue = 0,
            UserId = 0
        };

        //Act
        var result = _createValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.BidValue);
        result.ShouldHaveValidationErrorFor(x => x.UserId);
    }
}