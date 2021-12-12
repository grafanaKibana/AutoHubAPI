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
                new LotBidController(_bidServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetLotBids_LotExists_ReturnsOk()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            var bids = _fixture.CreateMany<BidResponseDTO>();
            var mappedBids = bids.Select(bidDTO => _fixture.Build<BidResponseModel>()
                .With(x => x.BidId, bidDTO.BidId)
                .With(x => x.BidTime, bidDTO.BidTime)
                .With(x => x.BidValue, bidDTO.BidValue)
                .Create());

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<BidResponseModel>>(bids)).Returns(mappedBids);
            _bidServiceMock.Setup(service => service.GetLotBids(lotId)).Returns(bids);

            //Act
            var result = _lotBidController.GetLotBids(lotId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void CreateBid_ValidData_ReturnsCreated()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            var requestModel = _fixture.Create<BidCreateRequestModel>();
            var mappedBid = _fixture.Build<BidCreateRequestDTO>()
                .With(x => x.UserId, requestModel.UserId)
                .With(x => x.BidValue, requestModel.BidValue)
                .Create();

            _mapperMock.Setup(mapper => mapper.Map<BidCreateRequestDTO>(requestModel)).Returns(mappedBid);

            //Act
            var result = _lotBidController.CreateBid(lotId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));

            _bidServiceMock.Verify(service => service.Create(lotId, mappedBid));
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
    }
}