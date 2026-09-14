using AutoFixture;
using AutoHub.API.Models.CarModelModels;
using AutoHub.API.Validators.ModelValidators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests;

public class CarModelValidatorTests
{
    private readonly Fixture fixture;
    private readonly CarModelCreateRequestModelValidator createModelValidator;
    private readonly CarModelUpdateRequestModelValidator updateModelValidator;

    public CarModelValidatorTests()
    {
        fixture = new Fixture();
        createModelValidator = new CarModelCreateRequestModelValidator();
        updateModelValidator = new CarModelUpdateRequestModelValidator();
    }

    [Fact]
    public void CreateModelTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = fixture.Create<CarModelCreateRequest>();

        //Act
        var result = createModelValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarModelName);
    }

    [Fact]
    public void CreateModelTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarModelCreateRequest { CarModelName = null };

        //Act
        var result = createModelValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarModelName);
    }

    [Fact]
    public void UpdateModelTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = fixture.Create<CarModelUpdateRequest>();

        //Act
        var result = updateModelValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarModelName);
    }

    [Fact]
    public void UpdateModelTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarModelUpdateRequest { CarModelName = null };

        //Act
        var result = updateModelValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarModelName);
    }
}
