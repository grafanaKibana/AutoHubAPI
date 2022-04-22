using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.LotModels;
using AutoHub.BusinessLogic.DTOs.LotDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.Domain.Enums;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AutoHub.Tests.ControllersTests;

public class LotControllerTests
{
    private readonly Fixture _fixture;
    private readonly LotController _lotController;
    private readonly Mock<ILotService> _lotServiceMock;
    private readonly Mock<IMapper> _mapperMock;

    public LotControllerTests()
    {
        _fixture = new Fixture();
        _mapperMock = new Mock<IMapper>();
        _lotServiceMock = new Mock<ILotService>();
        _lotController = new LotController(_lotServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAllLots_ReturnsOkAsync()
    {
        //Arrange
        var lots = _fixture.CreateMany<LotResponseDTO>();
        var mappedLots = lots.Select(lotDTO => _fixture.Build<LotResponse>()
            .With(x => x.EndTime, lotDTO.EndTime)
            .With(x => x.LotId, lotDTO.LotId)
            .With(x => x.LotStatus, lotDTO.LotStatus)
            .With(x => x.StartTime, lotDTO.StartTime)
            .Create());

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<LotResponse>>(lots)).Returns(mappedLots);
        _lotServiceMock.Setup(service => service.GetAll()).ReturnsAsync(lots);

        //Act
        var result = await _lotController.GetAllLots();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetActiveLots_ReturnsOkAsync()
    {
        //Arrange
        var activeLots = _fixture.Build<LotResponseDTO>()
            .With(x => x.LotStatus, LotStatusEnum.InProgress.ToString)
            .CreateMany();
        var mappedLots = activeLots.Select(lotDTO => _fixture.Build<LotResponse>()
            .With(x => x.EndTime, lotDTO.EndTime)
            .With(x => x.LotId, lotDTO.LotId)
            .With(x => x.LotStatus, lotDTO.LotStatus)
            .With(x => x.StartTime, lotDTO.StartTime)
            .Create());

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<LotResponse>>(activeLots)).Returns(mappedLots);
        _lotServiceMock.Setup(service => service.GetInProgress()).ReturnsAsync(activeLots);

        //Act
        var result = await _lotController.GetLotsInProgress();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetLotById_LotExists_ReturnsOkAsync()
    {
        //Arrange
        var lot = _fixture.Create<LotResponseDTO>();

        _lotServiceMock.Setup(service => service.GetById(lot.LotId)).ReturnsAsync(lot);

        //Act
        var result = await _lotController.GetLotById(lot.LotId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task CreateLot_ValidModel_ReturnsCreatedAsync()
    {
        //Arrange
        var requestModel = _fixture.Create<LotCreateRequest>();
        var mappedLot = _fixture.Build<LotCreateRequestDTO>()
            .With(x => x.CarId, requestModel.CarId)
            .With(x => x.CreatorId, requestModel.CreatorId)
            .With(x => x.DurationInDays, requestModel.DurationInDays)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<LotCreateRequestDTO>(requestModel)).Returns(mappedLot);
        //Act
        var result = await _lotController.CreateLot(requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));

        _lotServiceMock.Verify(service => service.Create(mappedLot));
    }

    [Fact]
    public async Task UpdateCar_ValidData_ReturnsNoContentAsync()
    {
        //Arrange
        var lotId = _fixture.Create<int>();
        var requestModel = _fixture.Build<LotUpdateRequest>()
            .With(x => x.LotStatusId,
                _fixture.Create<int>() % (4 - 1 + 1) + 1) //Defines range of generating to match enum values
            .Create();                                    //.. % (maxIdOfRole - minIdOfRole + 1) + minIdOfRole

        var mappedLot = _fixture.Build<LotUpdateRequestDTO>()
            .With(x => x.LotStatusId, requestModel.LotStatusId)
            .With(x => x.WinnerId, requestModel.DurationInDays)
            .With(x => x.DurationInDays, requestModel.DurationInDays)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<LotUpdateRequestDTO>(requestModel)).Returns(mappedLot);
        _lotServiceMock.Setup(service => service.Update(lotId, mappedLot));

        //Act
        var result = await _lotController.UpdateLot(lotId, requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();
    }

    [Fact]
    public async Task DeleteLot_LotExists_ReturnsNoContentAsync()
    {
        //Arrange
        var lotId = _fixture.Create<int>();

        //Act
        var result = await _lotController.DeleteLot(lotId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _lotServiceMock.Verify(service => service.Delete(lotId));
    }
}