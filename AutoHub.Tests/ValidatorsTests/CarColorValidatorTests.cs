using AutoFixture;
using AutoHub.API.Models.CarColorModels;
using AutoHub.API.Validators.ModelValidators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests;

public class CarColorValidatorTests
{
    private readonly Fixture fixture;
    private readonly CarColorCreateRequestModelValidator createValidator;
    private readonly CarColorUpdateRequestModelValidator updateValidator;

    public CarColorValidatorTests()
    {
        fixture = new Fixture();
        createValidator = new CarColorCreateRequestModelValidator();
        updateValidator = new CarColorUpdateRequestModelValidator();
    }

    [Fact]
    public void CreateColorTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = fixture.Create<CarColorCreateRequest>();

        //Act
        var result = createValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarColorName);
    }

    [Fact]
    public void CreateColorTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarColorCreateRequest { CarColorName = null };

        //Act
        var result = createValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarColorName);
    }

    [Fact]
    public void UpdateColorTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = fixture.Create<CarColorUpdateRequest>();

        //Act
        var result = updateValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarColorName);
    }

    [Fact]
    public void UpdateColorTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarColorUpdateRequest { CarColorName = null };

        //Act
        var result = updateValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarColorName);
    }
}
