using System.Collections.Generic;
using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.UserModels;
using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AutoHub.Tests.ControllersTests
{
    public class UserControllerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserController _userController;
        private readonly Mock<IUserService> _userServiceMock;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _mapperMock = new Mock<IMapper>();
            _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public void GetAllUsers_ReturnsOk()
        {
            //Arrange
            var users = _fixture.Create<IEnumerable<UserResponseDTO>>();
            _userServiceMock.Setup(repo => repo.GetAll()).Returns(users);

            //Act
            var result = _userController.GetAllUsers();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetById_UserExists_ReturnsOk()
        {
            //Arrange
            var user = _fixture.Create<UserResponseDTO>();
            _userServiceMock.Setup(service => service.GetById(user.UserId)).Returns(user);

            //Act
            var result = _userController.GetUserById(user.UserId);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetById_UserNotExists_ReturnsNotFound()
        {
            //Arrange
            _userServiceMock.Setup(service => service.GetById(It.IsAny<int>())).Returns(null as UserResponseDTO);

            //Act
            var result = _userController.GetUserById(int.MaxValue);

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void LoginUser_ValidData_ReturnsOk()
        {
            //Arrange
            var requestModel = _fixture.Create<UserLoginRequestModel>();

            var responseDTO = _fixture.Build<UserResponseDTO>()
                .With(x => x.Email, requestModel.Email)
                .Create();

            var mappedUser = _fixture.Build<UserLoginRequestDTO>()
                .With(x => x.Email, requestModel.Email)
                .With(x => x.Password, requestModel.Password)
                .Create();

            var authModel = _fixture.Build<UserLoginResponseDTO>()
                .With(x => x.Email, mappedUser.Email)
                .Create();

            var mappedAuthModel = _fixture.Build<UserLoginResponseModel>()
                .With(x => x.Email, authModel.Email)
                .With(x => x.Token, authModel.Token)
                .Create();

            _userServiceMock.Setup(service => service.GetByEmail(requestModel.Email)).Returns(responseDTO);
            _mapperMock.Setup(mapper => mapper.Map<UserLoginRequestDTO>(requestModel)).Returns(mappedUser);
            _userServiceMock.Setup(service => service.Login(mappedUser)).Returns(authModel);
            _mapperMock.Setup(mapper => mapper.Map<UserLoginResponseModel>(authModel)).Returns(mappedAuthModel);

            //Act
            var result = _userController.LoginUser(requestModel) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();

            // Assert.Equal( result.StatusCode, StatusCodes.Status200OK );
            // Assert.Equal( result.Email, requestModel.Email );
            //FluentAssertions Assert.AreEqual analog
            //How to get from result its content
        }

        [Fact]
        public void LoginUser_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var invalidModel = _fixture.Create<object>() as UserLoginRequestModel;

            //Act
            var result = _userController.LoginUser(invalidModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void LoginUser_UserNotExist_ReturnsNotFound()
        {
            //Arrange
            var requestModel = _fixture.Create<UserLoginRequestModel>();

            _userServiceMock.Setup(service => service.GetByEmail(requestModel.Email)).Returns(null as UserResponseDTO);

            //Act
            var result = _userController.LoginUser(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should()
                .BeOfType<NotFoundObjectResult>(); //What should it return? NotFoundResult or NotFoundObjectResult?
        }

        [Fact]
        public void RegisterUser_ValidModel_ReturnsCreated()
        {
            //Arrange
            var requestModel = _fixture.Create<UserRegisterRequestModel>();

            var mappedUser = _fixture.Build<UserRegisterRequestDTO>()
                .With(x => x.Email, requestModel.Email)
                .With(x => x.Password, requestModel.Password)
                .With(x => x.Phone, requestModel.Phone)
                .With(x => x.FirstName, requestModel.FirstName)
                .With(x => x.LastName, requestModel.LastName)
                .Create();

            _userServiceMock.Setup(service => service.Register(mappedUser));

            //Act
            var result = _userController.RegisterUser(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should()
                .BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));
        }

        [Fact]
        public void RegisterUser_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var requestModel = _fixture.Create<object>() as UserRegisterRequestModel;

            //Act
            var result = _userController.RegisterUser(requestModel);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateUser_WithExistingUser_ReturnsNoContent()
        {
            //Arrange
            var userId = _fixture.Create<int>();
            var requestModel = _fixture.Build<UserUpdateRequestModel>().Without(x => x.UserRoleId)
                .With(x => x.UserRoleId,
                    _fixture.Create<int>() % (3 - 1 + 1) + 1) //.. % (maxIdOfRole - minIdOfRole + 1) + minIdOfRole;
                .Create();

            var mappedUser = _fixture.Build<UserUpdateRequestDTO>()
                .With(x => x.Email, requestModel.Email)
                .With(x => x.Password, requestModel.Password)
                .With(x => x.Phone, requestModel.Phone)
                .With(x => x.FirstName, requestModel.FirstName)
                .With(x => x.LastName, requestModel.LastName)
                .With(x => x.UserRoleId, requestModel.UserRoleId)
                .Create();

            _userServiceMock.Setup(service => service.GetById(userId)).Returns(new UserResponseDTO());
            _userServiceMock.Setup(service => service.Update(userId, mappedUser));

            //Act
            var result = _userController.UpdateUser(userId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void UpdateUser_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var userId = _fixture.Create<int>();
            var requestModel = _fixture.Create<object>() as UserUpdateRequestModel;

            //Act
            var result = _userController.UpdateUser(userId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateUser_UserNotExist_ReturnsNotFound()
        {
            //Arrange
            var userId = _fixture.Create<int>();
            var requestModel = _fixture.Create<UserUpdateRequestModel>();

            _userServiceMock.Setup(service => service.GetById(userId)).Returns(null as UserResponseDTO);

            //Act
            var result = _userController.UpdateUser(userId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void UpdateUser_IncorrectStatusId_ReturnsUnprocessableEntity()
        {
            //Arrange
            var userId = _fixture.Create<int>();
            //var userRoleValues = Enum.GetValues(typeof(UserRoleEnum)) as int[];

            var requestModel = _fixture.Build<UserUpdateRequestModel>()
                .With(x => x.UserRoleId, int.MaxValue)
                .Create();

            var userResponseDTO = _fixture.Build<UserResponseDTO>()
                .With(x => x.UserId, userId)
                .Create();

            _userServiceMock.Setup(service => service.GetById(userId)).Returns(userResponseDTO);

            //Act
            var result = _userController.UpdateUser(userId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<UnprocessableEntityObjectResult>();
        }

        [Fact]
        public void DeleteUser_UserExist_ReturnsNoContent()
        {
            //Arrange
            var userId = _fixture.Create<int>();
            var userDTO = _fixture.Build<UserResponseDTO>()
                .With(x => x.UserId, userId)
                .Create();

            _userServiceMock.Setup(service => service.GetById(userId)).Returns(userDTO);
            _userServiceMock.Setup(service => service.Delete(userId));

            //Act
            var result = _userController.DeleteUser(userId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void DeleteUser_UserNotExist_ReturnsNotFound()
        {
            //Arrange
            var userId = _fixture.Create<int>();

            _userServiceMock.Setup(service => service.GetById(userId)).Returns(null as UserResponseDTO);

            //Act
            var result = _userController.DeleteUser(userId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}