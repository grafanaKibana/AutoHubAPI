using AutoFixture;
using AutoHub.API.Models.CarColorModels;
using AutoHub.API.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests
{
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
            var model = _fixture.Create<CarColorCreateRequestModel>();

            //Act
            var result = _createValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.CarColorName);
        }

        [Fact]
        public void CreateColorTestValidate_InvalidModel_ShouldHaveError()
        {
            //Arrange
            var model = new CarColorCreateRequestModel { CarColorName = null };

            //Act
            var result = _createValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.CarColorName);
        }

        [Fact]
        public void UpdateColorTestValidate_ValidModel_ShouldNotHaveError()
        {
            //Arrange
            var model = _fixture.Create<CarColorUpdateRequestModel>();

            //Act
            var result = _updateValidator.TestValidate(model);

            //Assert
            result.ShouldNotHaveValidationErrorFor(x => x.CarColorName);
        }

        [Fact]
        public void UpdateColorTestValidate_InvalidModel_ShouldHaveError()
        {
            //Arrange
            var model = new CarColorUpdateRequestModel { CarColorName = null };

            //Act
            var result = _updateValidator.TestValidate(model);

            //Assert
            result.ShouldHaveValidationErrorFor(x => x.CarColorName);
        }
    }
}