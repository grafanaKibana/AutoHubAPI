using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.BidModels;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.DTOs.LotDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AutoHub.Tests.ControllersTests
{
    public class LotBidControllerTests
    {
        private readonly Mock<IBidService> _bidServiceMock;
        private readonly Fixture _fixture;
        private readonly LotBidController _lotBidController;
        private readonly Mock<ILotService> _lotServiceMock;
        private readonly Mock<IMapper> _mapperMock;

        public LotBidControllerTests()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _bidServiceMock = new Mock<IBidService>();
            _lotServiceMock = new Mock<ILotService>();
            _lotBidController =
                new LotBidController(_bidServiceMock.Object, _lotServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetLotBids_LotExists_ReturnsOk()
        {
            //Arrange
            var lot = _fixture.Create<LotResponseDTO>();
            var bids = _fixture.CreateMany<BidResponseDTO>();

            _lotServiceMock.Setup(service => service.GetById(lot.LotId)).Returns(lot);
            _bidServiceMock.Setup(service => service.GetLotBids(lot.LotId)).Returns(bids);

            //Act
            var result = _lotBidController.GetLotBids(lot.LotId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetLotBids_LotNotExists_ReturnsNotFound()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            _lotServiceMock.Setup(service => service.GetById(lotId)).Returns(null as LotResponseDTO);

            //Act
            var result = _lotBidController.GetLotBids(lotId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void CreateBid_ValidData_ReturnsCreated()
        {
            //Arrange
            var lot = _fixture.Create<LotResponseDTO>();
            var requestModel = _fixture.Create<BidCreateRequestModel>();
            var mappedBid = _fixture.Build<BidCreateRequestDTO>()
                .With(x => x.UserId, requestModel.UserId)
                .With(x => x.BidValue, requestModel.BidValue)
                .Create();

            _lotServiceMock.Setup(service => service.GetById(lot.LotId)).Returns(lot);
            _bidServiceMock.Setup(service => service.Create(lot.LotId, mappedBid));

            //Act
            var result = _lotBidController.CreateBid(lot.LotId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));
        }

        [Fact]
        public void CreateBid_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            var requestModel = _fixture.Create<object>() as BidCreateRequestModel;

            //Act
            var result = _lotBidController.CreateBid(lotId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void CreateBid_LotNotExists_ReturnsNotFound()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            var requestModel = _fixture.Create<BidCreateRequestModel>();

            _lotServiceMock.Setup(service => service.GetById(lotId)).Returns(null as LotResponseDTO);

            //Act
            var result = _lotBidController.CreateBid(lotId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}