using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.CarBrandModels;
using AutoHub.BLL.DTOs.CarBrandDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
                .With(x => x.CarBrandName)
                .Create();

            _carBrandServiceMock.Setup(service => service.Create(mappedCarBrand));

            //Act
            var result = _carBrandController.CreateCarBrand(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));
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

            _carBrandServiceMock.Setup(service => service.GetById(carBrandId))
                .Returns(_fixture.Create<CarBrandResponseDTO>());
            _carBrandServiceMock.Setup(service => service.Update(carBrandId, mappedCarBrand));

            //Act
            var result = _carBrandController.UpdateCarBrand(carBrandId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UpdateCarBrand_InvalidData_ReturnsBadRequest()
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
        public void UpdateCarBrand_CarBrandNotExist_ReturnsNotFound()
        {
            //Arrange
            var carBrandId = _fixture.Create<int>();
            var requestModel = _fixture.Create<CarBrandUpdateRequestModel>();

            _carBrandServiceMock.Setup(service => service.GetById(carBrandId)).Returns(null as CarBrandResponseDTO);

            //Act
            var result = _carBrandController.UpdateCarBrand(carBrandId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteCarBrand_CarBrandExists_ReturnsNoContent()
        {
            //Arrange
            var carBrandId = _fixture.Create<int>();
            var carBrandResponseDTO = _fixture.Build<CarBrandResponseDTO>()
                .With(x => x.CarBrandId, carBrandId)
                .Create();

            _carBrandServiceMock.Setup(service => service.GetById(carBrandId)).Returns(carBrandResponseDTO);
            _carBrandServiceMock.Setup(service => service.Delete(carBrandId));

            //Act
            var result = _carBrandController.DeleteCarBrand(carBrandId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteCarBrand_CarBrandNotExists_ReturnsNotFound()
        {
            //Arrange
            var carBrandId = _fixture.Create<int>();

            _carBrandServiceMock.Setup(service => service.GetById(carBrandId)).Returns(null as CarBrandResponseDTO);

            //Act
            var result = _carBrandController.DeleteCarBrand(carBrandId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}