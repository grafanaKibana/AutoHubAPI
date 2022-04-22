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

public class UserBidControllerTests
{
    private readonly Mock<IBidService> _bidServiceMock;
    private readonly Fixture _fixture;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UserBidController _userBidController;

    public UserBidControllerTests()
    {
        _fixture = new Fixture();
        _mapperMock = new Mock<IMapper>();
        _bidServiceMock = new Mock<IBidService>();
        _userBidController =
            new UserBidController(_bidServiceMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task GetUserBids_UserExists_ReturnsOkAsync()
    {
        //Arrange
        var userId = _fixture.Create<int>();
        var bids = _fixture.CreateMany<BidResponseDTO>();
        var mappedBids = bids.Select(bidDTO => _fixture.Build<BidResponse>()
            .With(x => x.BidId, bidDTO.BidId)
            .With(x => x.BidTime, bidDTO.BidTime)
            .With(x => x.BidValue, bidDTO.BidValue)
            .Create());

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<BidResponse>>(bids)).Returns(mappedBids);
        _bidServiceMock.Setup(service => service.GetUserBids(userId)).ReturnsAsync(bids);

        //Act
        var result = await _userBidController.GetUserBids(userId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }
}