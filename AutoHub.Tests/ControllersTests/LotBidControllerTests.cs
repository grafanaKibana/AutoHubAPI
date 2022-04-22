using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.BidModels;
using AutoHub.BusinessLogic.DTOs.BidDTOs;
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

public class LotBidControllerTests
{
    private readonly Mock<IBidService> _bidServiceMock;
    private readonly Fixture _fixture;
    private readonly LotBidController _lotBidController;
    private readonly Mock<IMapper> _mapperMock;

    public LotBidControllerTests()
    {
        _fixture = new Fixture();
        _mapperMock = new Mock<IMapper>();
        _bidServiceMock = new Mock<IBidService>();
        _lotBidController = new LotBidController(_bidServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetLotBids_LotExists_ReturnsOkAsync()
    {
        //Arrange
        var lotId = _fixture.Create<int>();
        var bids = _fixture.CreateMany<BidResponseDTO>();
        var mappedBids = bids.Select(bidDTO => _fixture.Build<BidResponse>()
            .With(x => x.BidId, bidDTO.BidId)
            .With(x => x.BidTime, bidDTO.BidTime)
            .With(x => x.BidValue, bidDTO.BidValue)
            .Create());

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<BidResponse>>(bids)).Returns(mappedBids);
        _bidServiceMock.Setup(service => service.GetLotBids(lotId)).ReturnsAsync(bids);

        //Act
        var result = await _lotBidController.GetLotBids(lotId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task CreateBid_ValidData_ReturnsCreatedAsync()
    {
        //Arrange
        var lotId = _fixture.Create<int>();
        var requestModel = _fixture.Create<BidCreateRequest>();
        var mappedBid = _fixture.Build<BidCreateRequestDTO>()
            .With(x => x.UserId, requestModel.UserId)
            .With(x => x.BidValue, requestModel.BidValue)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<BidCreateRequestDTO>(requestModel)).Returns(mappedBid);

        //Act
        var result = await _lotBidController.CreateBid(lotId, requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));

        _bidServiceMock.Verify(service => service.Create(lotId, mappedBid));
    }
}