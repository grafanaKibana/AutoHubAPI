using AutoFixture;
using AutoHub.API.Models.CarColorModels;
using AutoHub.API.Validators.ModelValidators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests;

public class CarColorValidatorTests
{
    private readonly Fixture _fixture;
    private readonly CarColorCreateRequestModelValidator _createValidator;
    private readonly CarColorUpdateRequestModelValidator _updateValidator;

    public CarColorValidatorTests()
    {
        _fixture = new Fixture();
        _createValidator = new CarColorCreateRequestModelValidator();
        _updateValidator = new CarColorUpdateRequestModelValidator();
    }

    [Fact]
    public void CreateColorTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = _fixture.Create<CarColorCreateRequest>();

        //Act
        var result = _createValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarColorName);
    }

    [Fact]
    public void CreateColorTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarColorCreateRequest { CarColorName = null };

        //Act
        var result = _createValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarColorName);
    }

    [Fact]
    public void UpdateColorTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = _fixture.Create<CarColorUpdateRequest>();

        //Act
        var result = _updateValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarColorName);
    }

    [Fact]
    public void UpdateColorTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarColorUpdateRequest { CarColorName = null };

        //Act
        var result = _updateValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarColorName);
    }
}
