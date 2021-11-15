using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
                new UserBidController(_bidServiceMock.Object, _userServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetUserBids_UserExists_ReturnsOk()
        {
            //Arrange
            var user = _fixture.Create<UserResponseDTO>();
            var bids = _fixture.CreateMany<BidResponseDTO>();

            _userServiceMock.Setup(service => service.GetById(user.UserId)).Returns(user);
            _bidServiceMock.Setup(service => service.GetUserBids(user.UserId)).Returns(bids);

            //Act
            var result = _userBidController.GetUserBids(user.UserId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetUserBids_UserNotExists_ReturnsNotFound()
        {
            //Arrange
            var userId = _fixture.Create<int>();
            _userServiceMock.Setup(service => service.GetById(userId)).Returns(null as UserResponseDTO);

            //Act
            var result = _userBidController.GetUserBids(userId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}