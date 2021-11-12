using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.CarModelModels;
using AutoHub.BLL.DTOs.CarModelDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AutoHub.Tests.ControllersTests
{
    public class CarModelControllerTests
    {
        private readonly CarModelController _carModelController;
        private readonly Mock<ICarModelService> _carModelServiceMock;
        private readonly Fixture _fixture;
        private readonly Mock<IMapper> _mapperMock;

        public CarModelControllerTests()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _carModelServiceMock = new Mock<ICarModelService>();
            _carModelController = new CarModelController(_carModelServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAllCarModels_ReturnsOk()
        {
            //Arrange
            var carModels = _fixture.CreateMany<CarModelResponseDTO>();
            _carModelServiceMock.Setup(service => service.GetAll()).Returns(carModels);

            //Act
            var result = _carModelController.GetAllCarModels();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void CreateCarModel_ValidModel_ReturnsCreated()
        {
            //Arrange
            var requestModel = _fixture.Create<CarModelCreateRequestModel>();
            var mappedCarModel = _fixture.Build<CarModelCreateRequestDTO>()
                .With(x => x.CarModelName)
                .Create();

            _carModelServiceMock.Setup(service => service.Create(mappedCarModel));

            //Act
            var result = _carModelController.CreateCarModel(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));
        }

        [Fact]
        public void CreateCarModel_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var requestModel = _fixture.Create<object>() as CarModelCreateRequestModel;

            //Act
            var result = _carModelController.CreateCarModel(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateCarModel_ValidData_ReturnsNoContent()
        {
            //Arrange
            var carModelId = _fixture.Create<int>();
            var requestModel = _fixture.Create<CarModelUpdateRequestModel>();
            var mappedCarModel = _fixture.Build<CarModelUpdateRequestDTO>()
                .With(x => x.CarModelName, requestModel.CarModelName)
                .Create();

            _carModelServiceMock.Setup(service => service.GetById(carModelId))
                .Returns(_fixture.Create<CarModelResponseDTO>());
            _carModelServiceMock.Setup(service => service.Update(carModelId, mappedCarModel));

            //Act
            var result = _carModelController.UpdateCarModel(carModelId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UpdateCarModel_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var carModelId = _fixture.Create<int>();
            var requestModel = _fixture.Create<object>() as CarModelUpdateRequestModel;

            //Act
            var result = _carModelController.UpdateCarModel(carModelId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateCarModel_CarModelNotExist_ReturnsNotFound()
        {
            //Arrange
            var carModelId = _fixture.Create<int>();
            var requestModel = _fixture.Create<CarModelUpdateRequestModel>();

            _carModelServiceMock.Setup(service => service.GetById(carModelId)).Returns(null as CarModelResponseDTO);

            //Act
            var result = _carModelController.UpdateCarModel(carModelId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteCarModel_CarModelExists_ReturnsNoContent()
        {
            //Arrange
            var carModelId = _fixture.Create<int>();
            var carModelResponseDTO = _fixture.Build<CarModelResponseDTO>()
                .With(x => x.CarModelId, carModelId)
                .Create();

            _carModelServiceMock.Setup(service => service.GetById(carModelId)).Returns(carModelResponseDTO);
            _carModelServiceMock.Setup(service => service.Delete(carModelId));

            //Act
            var result = _carModelController.DeleteCarModel(carModelId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteCarModel_CarModelNotExists_ReturnsNotFound()
        {
            //Arrange
            var carModelId = _fixture.Create<int>();

            _carModelServiceMock.Setup(service => service.GetById(carModelId)).Returns(null as CarModelResponseDTO);

            //Act
            var result = _carModelController.DeleteCarModel(carModelId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}