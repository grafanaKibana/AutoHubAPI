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
    public void GetAllCarModels_ReturnsOk()
    {
        //Arrange
        var carModels = _fixture.CreateMany<CarModelResponseDTO>();
        var mappedCarModels = carModels.Select(modelDTO => _fixture.Build<CarModelResponse>()
            .With(x => x.CarModelId, modelDTO.CarModelId)
            .With(x => x.CarModelName, modelDTO.CarModelName)
            .Create());

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CarModelResponse>>(carModels))
            .Returns(mappedCarModels);
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
        var requestModel = _fixture.Create<CarModelCreateRequest>();
        var mappedCarModel = _fixture.Build<CarModelCreateRequestDTO>()
            .With(x => x.CarModelName)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<CarModelCreateRequestDTO>(requestModel)).Returns(mappedCarModel);

        //Act
        var result = _carModelController.CreateCarModel(requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));

        _carModelServiceMock.Verify(service => service.Create(mappedCarModel));
    }

    [Fact]
    public void UpdateCarModel_ValidData_ReturnsNoContent()
    {
        //Arrange
        var carModelId = _fixture.Create<int>();
        var requestModel = _fixture.Create<CarModelUpdateRequest>();
        var mappedCarModel = _fixture.Build<CarModelUpdateRequestDTO>()
            .With(x => x.CarModelName, requestModel.CarModelName)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<CarModelUpdateRequestDTO>(requestModel)).Returns(mappedCarModel);

        //Act
        var result = _carModelController.UpdateCarModel(carModelId, requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _carModelServiceMock.Verify(service => service.Update(carModelId, mappedCarModel));
    }

    [Fact]
    public void DeleteCarModel_CarModelExists_ReturnsNoContent()
    {
        //Arrange
        var carModelId = _fixture.Create<int>();

        //Act
        var result = _carModelController.DeleteCarModel(carModelId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _carModelServiceMock.Verify(service => service.Delete(carModelId));
    }
}
