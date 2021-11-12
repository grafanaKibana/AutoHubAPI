using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.CarColorModels;
using AutoHub.BLL.DTOs.CarColorDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AutoHub.Tests.ControllersTests
{
    public class CarColorControllerTests
    {
        private readonly CarColorController _carColorController;
        private readonly Mock<ICarColorService> _carColorServiceMock;
        private readonly Fixture _fixture;
        private readonly Mock<IMapper> _mapperMock;

        public CarColorControllerTests()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _carColorServiceMock = new Mock<ICarColorService>();
            _carColorController = new CarColorController(_carColorServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAllCarColors_ReturnsOk()
        {
            //Arrange
            var carColors = _fixture.CreateMany<CarColorResponseDTO>();
            _carColorServiceMock.Setup(service => service.GetAll()).Returns(carColors);

            //Act
            var result = _carColorController.GetAllCarColors();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void CreateCarColor_ValidModel_ReturnsCreated()
        {
            //Arrange
            var requestModel = _fixture.Create<CarColorCreateRequestModel>();
            var mappedCarColor = _fixture.Build<CarColorCreateRequestDTO>()
                .With(x => x.CarColorName, requestModel.CarColorName)
                .Create();

            _carColorServiceMock.Setup(service => service.Create(mappedCarColor));

            //Act
            var result = _carColorController.CreateCarColor(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));
        }

        [Fact]
        public void CreateCarColor_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var requestModel = _fixture.Create<object>() as CarColorCreateRequestModel;

            //Act
            var result = _carColorController.CreateCarColor(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateCarColor_ValidData_ReturnsNoContent()
        {
            //Arrange
            var carColorId = _fixture.Create<int>();
            var requestModel = _fixture.Create<CarColorUpdateRequestModel>();
            var mappedCarColor = _fixture.Build<CarColorUpdateRequestDTO>()
                .With(x => x.CarColorName, requestModel.CarColorName)
                .Create();

            _carColorServiceMock.Setup(service => service.GetById(carColorId))
                .Returns(_fixture.Create<CarColorResponseDTO>());
            _carColorServiceMock.Setup(service => service.Update(carColorId, mappedCarColor));

            //Act
            var result = _carColorController.UpdateCarColor(carColorId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UpdateCarColor_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var carColorId = _fixture.Create<int>();
            var requestModel = _fixture.Create<object>() as CarColorUpdateRequestModel;

            //Act
            var result = _carColorController.UpdateCarColor(carColorId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateCarColor_CarColorNotExists_ReturnsNotFound()
        {
            //Arrange
            var carColorId = _fixture.Create<int>();
            var requestModel = _fixture.Create<CarColorUpdateRequestModel>();

            _carColorServiceMock.Setup(service => service.GetById(carColorId)).Returns(null as CarColorResponseDTO);

            //Act
            var result = _carColorController.UpdateCarColor(carColorId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteCarColor_CarColorExists_ReturnsNoContent()
        {
            //Arrange
            var carColorId = _fixture.Create<int>();
            var carColorResponseDTO = _fixture.Build<CarColorResponseDTO>()
                .With(x => x.CarColorId, carColorId)
                .Create();

            _carColorServiceMock.Setup(service => service.GetById(carColorId)).Returns(carColorResponseDTO);
            _carColorServiceMock.Setup(service => service.Delete(carColorId));

            //Act
            var result = _carColorController.DeleteCarColor(carColorId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteCarColor_CarColorNotExists_ReturnsNotFound()
        {
            //Arrange
            var carColorId = _fixture.Create<int>();

            _carColorServiceMock.Setup(service => service.GetById(carColorId)).Returns(null as CarColorResponseDTO);

            //Act
            var result = _carColorController.DeleteCarColor(carColorId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}