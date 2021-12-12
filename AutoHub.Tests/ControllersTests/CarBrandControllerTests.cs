using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.CarBrandModels;
using AutoHub.BLL.DTOs.CarBrandDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoHub.Tests.ControllersTests
{
    public class CarBrandControllerTests
    {
        private readonly CarBrandController _carBrandController;
        private readonly Mock<ICarBrandService> _carBrandServiceMock;
        private readonly Fixture _fixture;
        private readonly Mock<IMapper> _mapperMock;

        public CarBrandControllerTests()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _carBrandServiceMock = new Mock<ICarBrandService>();
            _carBrandController = new CarBrandController(_carBrandServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAllCarBrands_ReturnsOk()
        {
            //Arrange
            var carBrands = _fixture.CreateMany<CarBrandResponseDTO>();
            var mappedCarBrands = carBrands.Select(brandDTO => _fixture.Build<CarBrandResponseModel>()
                .With(x => x.CarBrandId, brandDTO.CarBrandId)
                .With(x => x.CarBrandName, brandDTO.CarBrandName)
                .Create());

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CarBrandResponseModel>>(carBrands))
                .Returns(mappedCarBrands);
            _carBrandServiceMock.Setup(service => service.GetAll()).Returns(carBrands);

            //Act
            var result = _carBrandController.GetAllCarBrands();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void CreateCarBrand_ValidModel_ReturnsCreated()
        {
            //Arrange
            var requestModel = _fixture.Create<CarBrandCreateRequestModel>();
            var mappedCarBrand = _fixture.Build<CarBrandCreateRequestDTO>()
                .With(x => x.CarBrandName, requestModel.CarBrandName)
                .Create();

            _mapperMock.Setup(mapper => mapper.Map<CarBrandCreateRequestDTO>(requestModel))
                .Returns(mappedCarBrand);

            //Act
            var result = _carBrandController.CreateCarBrand(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));

            _carBrandServiceMock.Verify(service => service.Create(mappedCarBrand));
        }

        [Fact]
        public void CreateCarBrand_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var requestModel = _fixture.Create<object>() as CarBrandCreateRequestModel;

            //Act
            var result = _carBrandController.CreateCarBrand(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateCarBrand_ValidData_ReturnsNoContent()
        {
            //Arrange
            var carBrandId = _fixture.Create<int>();
            var requestModel = _fixture.Create<CarBrandUpdateRequestModel>();
            var mappedCarBrand = _fixture.Build<CarBrandUpdateRequestDTO>()
                .With(x => x.CarBrandName, requestModel.CarBrandName)
                .Create();

            _mapperMock.Setup(mapper => mapper.Map<CarBrandUpdateRequestDTO>(requestModel)).Returns(mappedCarBrand);

            //Act
            var result = _carBrandController.UpdateCarBrand(carBrandId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();

            _carBrandServiceMock.Verify(service => service.Update(carBrandId, mappedCarBrand));
        }

        [Fact]
        public void UpdateCarBrand_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var carBrandId = _fixture.Create<int>();
            var requestModel = _fixture.Create<object>() as CarBrandUpdateRequestModel;

            //Act
            var result = _carBrandController.UpdateCarBrand(carBrandId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void DeleteCarBrand_CarBrandExists_ReturnsNoContent()
        {
            //Arrange
            var carBrandId = _fixture.Create<int>();

            //Act
            var result = _carBrandController.DeleteCarBrand(carBrandId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();

            _carBrandServiceMock.Verify(service => service.Delete(carBrandId));
        }
    }
}