using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.LotModels;
using AutoHub.BLL.DTOs.LotDTOs;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Enums;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AutoHub.Tests.ControllersTests
{
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
        public void GetAllLots_ReturnsOk()
        {
            //Arrange
            var lots = _fixture.CreateMany<LotResponseDTO>();
            _lotServiceMock.Setup(service => service.GetAll()).Returns(lots);

            //Act
            var result = _lotController.GetAllLots();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetActiveLots_ReturnsOk()
        {
            //Arrange
            var activeLots = _fixture.Build<LotResponseDTO>()
                .With(x => x.LotStatus, LotStatusEnum.InProgress.ToString)
                .CreateMany();

            _lotServiceMock.Setup(service => service.GetActive()).Returns(activeLots);

            //Act
            var result = _lotController.GetActiveLots();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetLotById_LotExists_ReturnsOk()
        {
            //Arrange
            var lot = _fixture.Create<LotResponseDTO>();
            _lotServiceMock.Setup(service => service.GetById(lot.LotId)).Returns(lot);

            //Act
            var result = _lotController.GetLotById(lot.LotId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetLotById_LotNotExists_ReturnsNotFound()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            _lotServiceMock.Setup(service => service.GetById(lotId)).Returns(null as LotResponseDTO);

            //Act
            var result = _lotController.GetLotById(lotId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void CreateLot_ValidModel_ReturnsCreated()
        {
            //Arrange
            var lot = _fixture.Create<LotCreateRequestModel>();
            var mappedLot = _fixture.Build<LotCreateRequestDTO>()
                .With(x => x.CarId, lot.CarId)
                .With(x => x.CreatorId, lot.CreatorId)
                .With(x => x.DurationInDays, lot.DurationInDays)
                .Create();

            _lotServiceMock.Setup(service => service.Create(mappedLot));

            //Act
            var result = _lotController.CreateLot(lot);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));
        }

        [Fact]
        public void CreateLot_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var lot = _fixture.Create<object>() as LotCreateRequestModel;

            //Act
            var result = _lotController.CreateLot(lot);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateCar_ValidData_ReturnsNoContent()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            var requestModel = _fixture.Build<LotUpdateRequestModel>()
                .With(x => x.LotStatusId,
                    _fixture.Create<int>() % (4 - 1 + 1) + 1) //Defines range of generating to match enum values
                .Create(); //.. % (maxIdOfRole - minIdOfRole + 1) + minIdOfRole;


            var mappedLot = _fixture.Build<LotUpdateRequestDTO>()
                .With(x => x.LotStatusId, requestModel.LotStatusId)
                .With(x => x.WinnerId, requestModel.DurationInDays)
                .With(x => x.DurationInDays, requestModel.DurationInDays)
                .Create();

            _lotServiceMock.Setup(service => service.GetById(lotId)).Returns(new LotResponseDTO());
            _lotServiceMock.Setup(service => service.Update(lotId, mappedLot));

            //Act
            var result = _lotController.UpdateLot(lotId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UpdateLot_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            var requestModel = _fixture.Create<object>() as LotUpdateRequestModel;

            //Act
            var result = _lotController.UpdateLot(lotId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateLot_LotNotExist_ReturnsNotFound()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            var requestModel = _fixture.Create<LotUpdateRequestModel>();

            _lotServiceMock.Setup(service => service.GetById(lotId)).Returns(null as LotResponseDTO);

            //Act
            var result = _lotController.UpdateLot(lotId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void UpdateLot_IncorrectLotStatusId_ReturnsUnprocessableEntity()
        {
            //Arrange
            var lotId = _fixture.Create<int>();

            var requestModel = _fixture.Build<LotUpdateRequestModel>()
                .With(x => x.LotStatusId, int.MaxValue)
                .Create();

            var lotResponseDTO = _fixture.Build<LotResponseDTO>()
                .With(x => x.LotId, lotId)
                .Create();

            _lotServiceMock.Setup(service => service.GetById(lotId)).Returns(lotResponseDTO);

            //Act
            var result = _lotController.UpdateLot(lotId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnprocessableEntityResult>();
        }

        [Fact]
        public void DeleteLot_LotExists_ReturnsNoContent()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            var lotResponseDTO = _fixture.Build<LotResponseDTO>()
                .With(x => x.LotId, lotId)
                .Create();

            _lotServiceMock.Setup(service => service.GetById(lotId)).Returns(lotResponseDTO);
            _lotServiceMock.Setup(service => service.Delete(lotId));

            //Act
            var result = _lotController.DeleteLot(lotId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteLot_LotNotExists_ReturnsNotFound()
        {
            //Arrange
            var lotId = _fixture.Create<int>();
            _lotServiceMock.Setup(service => service.GetById(lotId)).Returns(null as LotResponseDTO);

            //Act
            var result = _lotController.DeleteLot(lotId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}