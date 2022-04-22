using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.CarModels;
using AutoHub.BusinessLogic.DTOs.CarDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AutoHub.Tests.ControllersTests;

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
    public async Task GetAllCars_ReturnsOkAsync()
    {
        //Arrange
        var cars = _fixture.CreateMany<CarResponseDTO>();
        var mappedCars = cars.Select(carDTO => _fixture.Build<CarResponse>()
            .With(x => x.CarId, carDTO.CarId)
            .With(x => x.CarBrand, carDTO.CarBrand)
            .With(x => x.CarModel, carDTO.CarModel)
            .With(x => x.CarColor, carDTO.CarColor)
            .With(x => x.ImgUrl, carDTO.ImgUrl)
            .With(x => x.Description, carDTO.Description)
            .With(x => x.Year, carDTO.Year)
            .With(x => x.VIN, carDTO.VIN)
            .With(x => x.Mileage, carDTO.Mileage)
            .With(x => x.SellingPrice, carDTO.SellingPrice)
            .With(x => x.CarStatus, carDTO.CarStatus)
            .Create());

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CarResponse>>(cars)).Returns(mappedCars);
        _carServiceMock.Setup(service => service.GetAll()).ReturnsAsync(cars);

        //Act
        var result = await _carController.GetAllCars();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetCarById_CarExists_ReturnsOkAsync()
    {
        //Arrange
        var car = _fixture.Create<CarResponseDTO>();
        var mappedCar = _fixture.Build<CarResponse>()
            .With(x => x.CarId, car.CarId)
            .With(x => x.CarBrand, car.CarBrand)
            .With(x => x.CarModel, car.CarModel)
            .With(x => x.CarColor, car.CarColor)
            .With(x => x.ImgUrl, car.ImgUrl)
            .With(x => x.Description, car.Description)
            .With(x => x.Year, car.Year)
            .With(x => x.VIN, car.VIN)
            .With(x => x.Mileage, car.Mileage)
            .With(x => x.SellingPrice, car.SellingPrice)
            .With(x => x.CarStatus, car.CarStatus)
            .Create();

        _carServiceMock.Setup(service => service.GetById(car.CarId)).ReturnsAsync(car);
        _mapperMock.Setup(mapper => mapper.Map<CarResponse>(car)).Returns(mappedCar);

        //Act
        var result = await _carController.GetCarById(car.CarId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task CreateCar_ValidModel_ReturnsCreatedAsync()
    {
        //Arrange
        var car = _fixture.Create<CarCreateRequest>();
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

        _mapperMock.Setup(mapper => mapper.Map<CarCreateRequestDTO>(car)).Returns(mappedCar);

        //Act
        var result = await _carController.CreateCar(car);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));

        _carServiceMock.Verify(service => service.Create(mappedCar));
    }

    [Fact]
    public async Task UpdateCar_ValidData_ReturnsNoContentAsync()
    {
        //Arrange
        var carId = _fixture.Create<int>();

        var requestModel = _fixture.Build<CarUpdateRequest>()
            .With(x => x.CarStatusId,
                _fixture.Create<int>() % (6 - 1 + 1) + 1) //Defines range of generating to match enum values
            .Create();                                    //.. % (maxIdOfRole - minIdOfRole + 1) + minIdOfRole

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

        _mapperMock.Setup(mapper => mapper.Map<CarUpdateRequestDTO>(requestModel)).Returns(mappedCar);

        //Act
        var result = await _carController.UpdateCar(carId, requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _carServiceMock.Verify(service => service.Update(carId, mappedCar));
    }

    [Fact]
    public async Task DeleteCar_CarExists_ReturnsNoContentAsync()
    {
        //Arrange
        var carId = _fixture.Create<int>();

        //Act
        var result = await _carController.DeleteCar(carId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _carServiceMock.Verify(service => service.Delete(carId));
    }
}