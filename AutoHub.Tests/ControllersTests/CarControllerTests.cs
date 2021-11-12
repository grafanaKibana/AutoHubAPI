using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.CarModels;
using AutoHub.BLL.DTOs.CarDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AutoHub.Tests.ControllersTests
{
    public class CarControllerTests
    {
        private readonly CarController _carController;
        private readonly Mock<ICarService> _carServiceMock;
        private readonly Fixture _fixture;
        private readonly Mock<IMapper> _mapperMock;

        public CarControllerTests()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _carServiceMock = new Mock<ICarService>();
            _carController = new CarController(_carServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAllCars_ReturnsOk()
        {
            //Arrange
            var cars = _fixture.CreateMany<CarResponseDTO>();
            _carServiceMock.Setup(service => service.GetAll()).Returns(cars);

            //Act
            var result = _carController.GetAllCars();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetCarById_CarExists_ReturnsOk()
        {
            //Arrange
            var car = _fixture.Create<CarResponseDTO>();
            _carServiceMock.Setup(service => service.GetById(car.CarId)).Returns(car);

            //Act
            var result = _carController.GetCarById(car.CarId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetCarById_CarNotExists_ReturnsNotFound()
        {
            //Arrange
            _carServiceMock.Setup(service => service.GetById(It.IsAny<int>())).Returns(null as CarResponseDTO);

            //Act
            var result = _carController.GetCarById(int.MaxValue);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void CreateCar_ValidModel_ReturnsCreated()
        {
            //Arrange
            var car = _fixture.Create<CarCreateRequestModel>();
            var mappedCar = _fixture.Build<CarCreateRequestDTO>()
                .With(x => x.CarBrand, car.CarBrand)
                .With(x => x.CarModel, car.CarModel)
                .With(x => x.CarColor, car.CarColor)
                .With(x => x.ImgUrl, car.ImgUrl)
                .With(x => x.Description, car.Description)
                .With(x => x.Year, car.Year)
                .With(x => x.VIN, car.VIN)
                .With(x => x.Mileage, car.Mileage)
                .With(x => x.SellingPrice, car.SellingPrice)
                .With(x => x.CostPrice, car.CostPrice)
                .Create();

            _carServiceMock.Setup(service => service.Create(mappedCar));

            //Act
            var result = _carController.CreateCar(car);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));
        }

        [Fact]
        public void CreateCar_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var requestModel = _fixture.Create<object>() as CarCreateRequestModel;

            //Act
            var result = _carController.CreateCar(requestModel);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateCar_WithValidData_ReturnsNoContent()
        {
            //Arrange
            var carId = _fixture.Create<int>();
            var requestModel = _fixture.Build<CarUpdateRequestModel>()
                .With(x => x.CarStatusId, _fixture.Create<int>() % (6 - 1 + 1) + 1) //To match enum values
                .Create(); //.. % (maxIdOfRole - minIdOfRole + 1) + minIdOfRole;
            var mappedCar = _fixture.Build<CarUpdateRequestDTO>()
                .With(x => x.CarBrand, requestModel.CarBrand)
                .With(x => x.CarModel, requestModel.CarModel)
                .With(x => x.CarColor, requestModel.CarColor)
                .With(x => x.ImgUrl, requestModel.ImgUrl)
                .With(x => x.Description, requestModel.Description)
                .With(x => x.Year, requestModel.Year)
                .With(x => x.VIN, requestModel.VIN)
                .With(x => x.Mileage, requestModel.Mileage)
                .With(x => x.SellingPrice, requestModel.SellingPrice)
                .With(x => x.CostPrice, requestModel.CostPrice)
                .With(x => x.CarStatusId, requestModel.CarStatusId)
                .Create();

            _carServiceMock.Setup(service => service.GetById(carId)).Returns(_fixture.Create<CarResponseDTO>());
            _carServiceMock.Setup(service => service.Update(carId, mappedCar));

            //Act
            var result = _carController.UpdateCar(carId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UpdateCar_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var carId = _fixture.Create<int>();
            var requestModel = _fixture.Create<object>() as CarUpdateRequestModel;

            //Act
            var result = _carController.UpdateCar(carId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateCar_CarNotExist_ReturnsNotFound()
        {
            //Arrange
            var carId = _fixture.Create<int>();
            var requestModel = _fixture.Create<CarUpdateRequestModel>();

            _carServiceMock.Setup(service => service.GetById(carId)).Returns(null as CarResponseDTO);

            //Act
            var result = _carController.UpdateCar(carId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void UpdateCar_IncorrectCarStatusId_ReturnsUnprocessableEntity()
        {
            //Arrange
            var carId = _fixture.Create<int>();
            var requestModel = _fixture.Build<CarUpdateRequestModel>()
                .With(x => x.CarStatusId, int.MaxValue)
                .Create();
            var responseDTO = _fixture.Build<CarResponseDTO>()
                .With(x => x.CarId, carId)
                .Create();

            _carServiceMock.Setup(service => service.GetById(carId)).Returns(responseDTO);

            //Act
            var result = _carController.UpdateCar(carId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Fact]
        public void DeleteCar_CarExists_ReturnsNoContent()
        {
            //Arrange
            var carId = _fixture.Create<int>();
            var carResponseDTO = _fixture.Build<CarResponseDTO>()
                .With(x => x.CarId, carId)
                .Create();

            _carServiceMock.Setup(service => service.GetById(carId)).Returns(carResponseDTO);
            _carServiceMock.Setup(service => service.Delete(carId));

            //Act
            var result = _carController.DeleteCar(carId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteCar_CarNotExists_ReturnsNotFound()
        {
            //Arrange
            var carId = _fixture.Create<int>();

            _carServiceMock.Setup(service => service.GetById(carId)).Returns(null as CarResponseDTO);

            //Act
            var result = _carController.DeleteCar(carId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}