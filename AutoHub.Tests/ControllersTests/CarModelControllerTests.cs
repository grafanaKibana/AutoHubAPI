using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.CarModelModels;
using AutoHub.BusinessLogic.DTOs.CarModelDTOs;
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
    public async Task GetAllCarModels_ReturnsOkAsync()
    {
        //Arrange
        var carModels = _fixture.CreateMany<CarModelResponseDTO>();
        var mappedCarModels = carModels.Select(modelDTO => _fixture.Build<CarModelResponse>()
            .With(x => x.CarModelId, modelDTO.CarModelId)
            .With(x => x.CarModelName, modelDTO.CarModelName)
            .Create());

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CarModelResponse>>(carModels))
            .Returns(mappedCarModels);
        _carModelServiceMock.Setup(service => service.GetAll()).ReturnsAsync(carModels);

        //Act
        var result = await _carModelController.GetAllCarModels();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task CreateCarModel_ValidModel_ReturnsCreatedAsync()
    {
        //Arrange
        var requestModel = _fixture.Create<CarModelCreateRequest>();
        var mappedCarModel = _fixture.Build<CarModelCreateRequestDTO>()
            .With(x => x.CarModelName)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<CarModelCreateRequestDTO>(requestModel)).Returns(mappedCarModel);

        //Act
        var result = await _carModelController.CreateCarModel(requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));

        _carModelServiceMock.Verify(service => service.Create(mappedCarModel));
    }

    [Fact]
    public async Task UpdateCarModel_ValidData_ReturnsNoContentAsync()
    {
        //Arrange
        var carModelId = _fixture.Create<int>();
        var requestModel = _fixture.Create<CarModelUpdateRequest>();
        var mappedCarModel = _fixture.Build<CarModelUpdateRequestDTO>()
            .With(x => x.CarModelName, requestModel.CarModelName)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<CarModelUpdateRequestDTO>(requestModel)).Returns(mappedCarModel);

        //Act
        var result = await _carModelController.UpdateCarModel(carModelId, requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _carModelServiceMock.Verify(service => service.Update(carModelId, mappedCarModel));
    }

    [Fact]
    public async Task DeleteCarModel_CarModelExists_ReturnsNoContentAsync()
    {
        //Arrange
        var carModelId = _fixture.Create<int>();

        //Act
        var result = await _carModelController.DeleteCarModel(carModelId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _carModelServiceMock.Verify(service => service.Delete(carModelId));
    }
}