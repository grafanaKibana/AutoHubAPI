using AutoFixture;
using AutoHub.API.Models.CarBrandModels;
using AutoHub.API.Validators.ModelValidators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests;

public class CarBrandValidatorTests
{
    private readonly Fixture fixture;
    private readonly CarBrandCreateRequestModelValidator createValidator;
    private readonly CarBrandUpdateRequestModelValidator updateValidator;

    public CarBrandValidatorTests()
    {
        fixture = new Fixture();
        createValidator = new CarBrandCreateRequestModelValidator();
        updateValidator = new CarBrandUpdateRequestModelValidator();
    }

    [Fact]
    public void CreateBrandTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = fixture.Create<CarBrandCreateRequest>();

        //Act
        var result = createValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarBrandName);
    }

    [Fact]
    public void CreateBrandTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarBrandCreateRequest { CarBrandName = null };

        //Act
        var result = createValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarBrandName);
    }

    [Fact]
    public void UpdateBrandTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = fixture.Create<CarBrandUpdateRequest>();

        //Act
        var result = updateValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarBrandName);
    }

    [Fact]
    public void UpdateBrandTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarBrandUpdateRequest { CarBrandName = null };

        //Act
        var result = updateValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CarBrandName);
    }
}
