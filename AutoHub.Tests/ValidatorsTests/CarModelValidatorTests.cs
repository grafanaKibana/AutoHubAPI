using AutoFixture;
using AutoHub.API.Models.CarModelModels;
using AutoHub.API.Validators.ModelValidators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests;

public class CarModelValidatorTests
{
    private readonly Fixture _fixture;
    private readonly CarModelCreateRequestModelValidator _createModelValidator;
    private readonly CarModelUpdateRequestModelValidator _updateModelValidator;

    public CarModelValidatorTests()
    {
        _fixture = new Fixture();
        _createModelValidator = new CarModelCreateRequestModelValidator();
        _updateModelValidator = new CarModelUpdateRequestModelValidator();
    }

    [Fact]
    public void CreateModelTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = _fixture.Create<CarModelCreateRequest>();

        //Act
        var result = _createModelValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarModelName);
    }

    [Fact]
    public void CreateModelTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarModelCreateRequest { CarModelName = null };

        //Act
        var result = _createModelValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarModelName);
    }

    [Fact]
    public void UpdateModelTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = _fixture.Create<CarModelUpdateRequest>();

        //Act
        var result = _updateModelValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarModelName);
    }

    [Fact]
    public void UpdateModelTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarModelUpdateRequest { CarModelName = null };

        //Act
        var result = _updateModelValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarModelName);
    }
}