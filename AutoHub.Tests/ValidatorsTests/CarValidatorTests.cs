using AutoHub.API.Models.CarModels;
using AutoHub.API.Validators.ModelValidators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests;

public class CarValidatorTests
{
    private readonly CarCreateRequestModelValidator _createValidator;
    private readonly CarUpdateRequestModelValidator _updateValidator;

    public CarValidatorTests()
    {
        _createValidator = new CarCreateRequestModelValidator();
        _updateValidator = new CarUpdateRequestModelValidator();
    }

    [Fact]
    public void CreateCarTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = new CarCreateRequest
        {
            CarBrand = "Audi",
            CarModel = "RS e-tron GT",
            CarColor = "Space Gray",
            CostPrice = 134500,
            SellingPrice = 141225,
            Mileage = 24631,
            VIN = "1HGCM66537A023172",
            Year = 2021,
            Description = "Description",
            ImgUrl = "audi.com/RS_etron_GT"
        };

        //Act
        var result = _createValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CostPrice);
        result.ShouldNotHaveValidationErrorFor(x => x.SellingPrice);
        result.ShouldNotHaveValidationErrorFor(x => x.Mileage);
        result.ShouldNotHaveValidationErrorFor(x => x.VIN);
        result.ShouldNotHaveValidationErrorFor(x => x.CarBrand);
        result.ShouldNotHaveValidationErrorFor(x => x.CarColor);
        result.ShouldNotHaveValidationErrorFor(x => x.CarModel);
        result.ShouldNotHaveValidationErrorFor(x => x.Year);
        result.ShouldNotHaveValidationErrorFor(x => x.Description);
        result.ShouldNotHaveValidationErrorFor(x => x.ImgUrl);
    }

    [Fact]
    public void CreateCarTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarCreateRequest
        {
            CarBrand = "",
            CarModel = "",
            CarColor = "",
            CostPrice = -240000,
            SellingPrice = -7101225,
            Mileage = -24631,
            VIN = "1HGCM66537A0231",
            Year = 1755,
            Description = "",
            ImgUrl = ""
        };

        //Act
        var result = _createValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CostPrice);
        result.ShouldHaveValidationErrorFor(x => x.SellingPrice);
        result.ShouldHaveValidationErrorFor(x => x.Mileage);
        result.ShouldHaveValidationErrorFor(x => x.VIN);
        result.ShouldHaveValidationErrorFor(x => x.CarBrand);
        result.ShouldHaveValidationErrorFor(x => x.CarColor);
        result.ShouldHaveValidationErrorFor(x => x.CarModel);
        result.ShouldHaveValidationErrorFor(x => x.Year);
    }

    [Fact]
    public void UpdateCarTestValidate_ValidModel_ShouldNotHaveError()
    {
        //Arrange
        var model = new CarUpdateRequest
        {
            CarStatusId = 2,
            CarBrand = "Audi",
            CarModel = "RS e-tron GT",
            CarColor = "Space Gray",
            CostPrice = 134500,
            SellingPrice = 141225,
            Mileage = 24631,
            VIN = "1HGCM66537A023172",
            Year = 2021,
            Description = "Description",
            ImgUrl = "audi.com/RS_etron_GT"
        };

        //Act
        var result = _updateValidator.TestValidate(model);

        //Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CarStatusId);
        result.ShouldNotHaveValidationErrorFor(x => x.CostPrice);
        result.ShouldNotHaveValidationErrorFor(x => x.SellingPrice);
        result.ShouldNotHaveValidationErrorFor(x => x.Mileage);
        result.ShouldNotHaveValidationErrorFor(x => x.VIN);
        result.ShouldNotHaveValidationErrorFor(x => x.CarBrand);
        result.ShouldNotHaveValidationErrorFor(x => x.CarColor);
        result.ShouldNotHaveValidationErrorFor(x => x.CarModel);
        result.ShouldNotHaveValidationErrorFor(x => x.Year);
        result.ShouldNotHaveValidationErrorFor(x => x.Description);
        result.ShouldNotHaveValidationErrorFor(x => x.ImgUrl);
    }

    [Fact]
    public void UpdateCarTestValidate_InvalidModel_ShouldHaveError()
    {
        //Arrange
        var model = new CarUpdateRequest
        {
            CarStatusId = 255,
            CarBrand = "",
            CarModel = "",
            CarColor = "",
            CostPrice = -240000,
            SellingPrice = -7101225,
            Mileage = -24631,
            VIN = "1HGCM66537A0231",
            Year = 1755,
            Description = "",
            ImgUrl = ""
        };

        //Act
        var result = _updateValidator.TestValidate(model);

        //Assert
        result.ShouldHaveValidationErrorFor(x => x.CostPrice);
        result.ShouldHaveValidationErrorFor(x => x.SellingPrice);
        result.ShouldHaveValidationErrorFor(x => x.Mileage);
        result.ShouldHaveValidationErrorFor(x => x.VIN);
        result.ShouldHaveValidationErrorFor(x => x.CarBrand);
        result.ShouldHaveValidationErrorFor(x => x.CarColor);
        result.ShouldHaveValidationErrorFor(x => x.CarModel);
        result.ShouldHaveValidationErrorFor(x => x.Year);
    }
}
