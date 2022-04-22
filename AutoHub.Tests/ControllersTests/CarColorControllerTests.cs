using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.CarColorModels;
using AutoHub.BusinessLogic.DTOs.CarColorDTOs;
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
    public async Task GetAllCarColors_ReturnsOkAsync()
    {
        //Arrange
        var carColors = _fixture.CreateMany<CarColorResponseDTO>();
        var mappedCarColors = carColors.Select(colorDTO =>
            _fixture.Build<CarColorResponse>()
                .With(x => x.CarColorId, colorDTO.CarColorId)
                .With(x => x.CarColorName, colorDTO.CarColorName)
                .Create());

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CarColorResponse>>(carColors))
            .Returns(mappedCarColors);
        _carColorServiceMock.Setup(service => service.GetAll()).ReturnsAsync(carColors);

        //Act
        var result = await _carColorController.GetAllCarColors();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task CreateCarColor_ValidModel_ReturnsCreatedAsync()
    {
        //Arrange
        var requestModel = _fixture.Create<CarColorCreateRequest>();
        var mappedCarColor = _fixture.Build<CarColorCreateRequestDTO>()
            .With(x => x.CarColorName, requestModel.CarColorName)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<CarColorCreateRequestDTO>(requestModel)).Returns(mappedCarColor);

        //Act
        var result = await _carColorController.CreateCarColor(requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));

        _carColorServiceMock.Verify(service => service.Create(mappedCarColor));
    }

    [Fact]
    public async Task UpdateCarColor_ValidData_ReturnsNoContentAsync()
    {
        //Arrange
        var carColorId = _fixture.Create<int>();
        var requestModel = _fixture.Create<CarColorUpdateRequest>();
        var mappedCarColor = _fixture.Build<CarColorUpdateRequestDTO>()
            .With(x => x.CarColorName, requestModel.CarColorName)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<CarColorUpdateRequestDTO>(requestModel)).Returns(mappedCarColor);

        //Act
        var result = await _carColorController.UpdateCarColor(carColorId, requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _carColorServiceMock.Verify(service => service.Update(carColorId, mappedCarColor));
    }

    [Fact]
    public async Task DeleteCarColor_CarColorExists_ReturnsNoContentAsync()
    {
        //Arrange
        var carColorId = _fixture.Create<int>();

        //Act
        var result = await _carColorController.DeleteCarColor(carColorId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _carColorServiceMock.Verify(service => service.Delete(carColorId));
    }
}