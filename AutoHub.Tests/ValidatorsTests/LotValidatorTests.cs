using AutoHub.API.Models.LotModels;
using AutoHub.API.Validators.ModelValidators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests;

public class LotValidatorTests
{
    private readonly LotCreateRequestModelValidator _createValidator;
    private readonly LotUpdateRequestModelValidator _updateValidator;

    public LotValidatorTests()
    {
        _createValidator = new LotCreateRequestModelValidator();
        _updateValidator = new LotUpdateRequestModelValidator();
    }

    [Fact]
    public void CreateLotTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = new LotCreateRequest
        {
            CarId = 1,
            CreatorId = 1,
            DurationInDays = 7
        };

        //Act
        var result = _createValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarId);
        result.ShouldNotHaveValidationErrorFor(x => x.CreatorId);
        result.ShouldNotHaveValidationErrorFor(x => x.DurationInDays);
    }

    [Fact]
    public void CreateLotTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new LotCreateRequest
        {
            CarId = -1,
            CreatorId = -1,
            DurationInDays = -1
        };

        //Act
        var result = _createValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarId);
        result.ShouldHaveValidationErrorFor(x => x.CreatorId);
        result.ShouldHaveValidationErrorFor(x => x.DurationInDays);
    }

    [Fact]
    public void UpdateLotTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = new LotUpdateRequest
        {
            DurationInDays = 7,
            LotStatusId = 2,
            WinnerId = 1
        };

        //Act
        var result = _updateValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.DurationInDays);
        result.ShouldNotHaveValidationErrorFor(x => x.LotStatusId);
        result.ShouldNotHaveValidationErrorFor(x => x.WinnerId);
    }

    [Fact]
    public void UpdateLotTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new LotUpdateRequest
        {
            DurationInDays = -1,
            LotStatusId = 22,
            WinnerId = null
        };

        //Act
        var result = _updateValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.DurationInDays);
        result.ShouldHaveValidationErrorFor(x => x.LotStatusId);
    }
}