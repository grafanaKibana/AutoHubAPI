using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHub.API.Models.BidModels;
using AutoHub.API.Validators;
using FluentValidation.TestHelper;
using Xunit;

namespace AutoHub.Tests.ValidatorsTests
{
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
            var model = new BidCreateRequestModel
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
            var model = new BidCreateRequestModel
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
}
