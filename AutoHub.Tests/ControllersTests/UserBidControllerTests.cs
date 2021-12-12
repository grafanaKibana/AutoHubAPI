using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.BidModels;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AutoHub.Tests.ControllersTests
{
    public class UserBidControllerTests
    {
        private readonly Mock<IBidService> _bidServiceMock;
        private readonly Fixture _fixture;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserBidController _userBidController;
        private readonly Mock<IUserService> _userServiceMock;

        public UserBidControllerTests()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _bidServiceMock = new Mock<IBidService>();
            _userServiceMock = new Mock<IUserService>();
            _userBidController =
                new UserBidController(_bidServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetUserBids_UserExists_ReturnsOk()
        {
            //Arrange
            var userId = _fixture.Create<int>();
            var bids = _fixture.CreateMany<BidResponseDTO>();
            var mappedBids = bids.Select(bidDTO => _fixture.Build<BidResponseModel>()
                .With(x => x.BidId, bidDTO.BidId)
                .With(x => x.BidTime, bidDTO.BidTime)
                .With(x => x.BidValue, bidDTO.BidValue)
                .Create());

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<BidResponseModel>>(bids)).Returns(mappedBids);
            _bidServiceMock.Setup(service => service.GetUserBids(userId)).Returns(bids);

            //Act
            var result = _userBidController.GetUserBids(userId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}