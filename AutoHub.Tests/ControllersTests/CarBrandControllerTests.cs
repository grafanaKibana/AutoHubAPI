using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.CarBrandModels;
using AutoHub.BusinessLogic.DTOs.CarBrandDTOs;
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
    public async Task GetAllCarBrands_ReturnsOkAsync()
    {
        //Arrange
        var carBrands = _fixture.CreateMany<CarBrandResponseDTO>();
        var mappedCarBrands = carBrands.Select(brandDTO => _fixture.Build<CarBrandResponse>()
            .With(x => x.CarBrandId, brandDTO.CarBrandId)
            .With(x => x.CarBrandName, brandDTO.CarBrandName)
            .Create());

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<CarBrandResponse>>(carBrands))
            .Returns(mappedCarBrands);
        _carBrandServiceMock.Setup(service => service.GetAll()).ReturnsAsync(carBrands);

        //Act
        var result = await _carBrandController.GetAllCarBrands();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task CreateCarBrand_ValidModel_ReturnsCreatedAsync()
    {
        //Arrange
        var requestModel = _fixture.Create<CarBrandCreateRequest>();
        var mappedCarBrand = _fixture.Build<CarBrandCreateRequestDTO>()
            .With(x => x.CarBrandName, requestModel.CarBrandName)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<CarBrandCreateRequestDTO>(requestModel))
            .Returns(mappedCarBrand);

        //Act
        var result = await _carBrandController.CreateCarBrand(requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));

        _carBrandServiceMock.Verify(service => service.Create(mappedCarBrand));
    }

    [Fact]
    public async Task UpdateCarBrand_ValidData_ReturnsNoContentAsync()
    {
        //Arrange
        var carBrandId = _fixture.Create<int>();
        var requestModel = _fixture.Create<CarBrandUpdateRequest>();
        var mappedCarBrand = _fixture.Build<CarBrandUpdateRequestDTO>()
            .With(x => x.CarBrandName, requestModel.CarBrandName)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<CarBrandUpdateRequestDTO>(requestModel)).Returns(mappedCarBrand);

        //Act
        var result = await _carBrandController.UpdateCarBrand(carBrandId, requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _carBrandServiceMock.Verify(service => service.Update(carBrandId, mappedCarBrand));
    }

    [Fact]
    public async Task DeleteCarBrand_CarBrandExists_ReturnsNoContentAsync()
    {
        //Arrange
        var carBrandId = _fixture.Create<int>();

        //Act
        var result = await _carBrandController.DeleteCarBrand(carBrandId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _carBrandServiceMock.Verify(service => service.Delete(carBrandId));
    }
}