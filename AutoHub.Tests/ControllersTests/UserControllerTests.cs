using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.UserModels;
using AutoHub.BLL.DTOs.UserDTOs;
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
    public class UserControllerTests
    {
        private readonly Fixture _fixture;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserController _userController;
        private readonly Mock<IUserService> _userServiceMock;


        public UserControllerTests()
        {
            _fixture = new Fixture();
            _mapperMock = new Mock<IMapper>();
            _userServiceMock = new Mock<IUserService>();
            _userController = new UserController(_userServiceMock.Object, _mapperMock.Object);
        }

        [Fact]
        public void GetAllUsers_ReturnsOk()
        {
            //Arrange
            var users = _fixture.CreateMany<UserResponseDTO>();
            var mappedUsers = users.Select(userDTO => _fixture.Build<UserResponseModel>()
                .With(x => x.Email, userDTO.Email)
                .With(x => x.Phone, userDTO.Phone)
                .With(x => x.FirstName, userDTO.FirstName)
                .With(x => x.LastName, userDTO.LastName)
                .With(x => x.UserId, userDTO.UserId)
                .With(x => x.UserRole, userDTO.UserRole)
                .With(x => x.RegistrationTime, userDTO.RegistrationTime)
                .Create());

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserResponseModel>>(users)).Returns(mappedUsers);
            _userServiceMock.Setup(service => service.GetAll()).Returns(users);

            //Act
            var result = _userController.GetAllUsers();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetByUserById_UserExists_ReturnsOk()
        {
            //Arrange
            var user = _fixture.Create<UserResponseDTO>();
            var mappedUser = _fixture.Build<UserResponseModel>()
                .With(x => x.Email, user.Email)
                .With(x => x.Phone, user.Phone)
                .With(x => x.FirstName, user.FirstName)
                .With(x => x.LastName, user.LastName)
                .With(x => x.UserId, user.UserId)
                .With(x => x.UserRole, user.UserRole)
                .With(x => x.RegistrationTime, user.RegistrationTime)
                .Create();

            _mapperMock.Setup(mapper => mapper.Map<UserResponseModel>(user)).Returns(mappedUser);
            _userServiceMock.Setup(service => service.GetById(user.UserId)).Returns(user);

            //Act
            var result = _userController.GetUserById(user.UserId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
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
            var result = _userController.LoginUser(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
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

            _mapperMock.Setup(mapper => mapper.Map<UserRegisterRequestDTO>(requestModel)).Returns(mappedUser);
            //Act
            var result = _userController.RegisterUser(requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));

            _userServiceMock.Verify(service => service.Register(mappedUser));
        }

        [Fact]
        public void UpdateUser_ValidData_ReturnsNoContent()
        {
            //Arrange
            var userId = _fixture.Create<int>();
            var requestModel = _fixture.Build<UserUpdateRequestModel>()
                .With(x => x.UserRoleId,
                    _fixture.Create<int>() % (3 - 1 + 1) + 1) //Defines range of generating to match enum values
                .Create(); //.. % (maxIdOfRole - minIdOfRole + 1) + minIdOfRole;

            var mappedUser = _fixture.Build<UserUpdateRequestDTO>()
                .With(x => x.Email, requestModel.Email)
                .With(x => x.Password, requestModel.Password)
                .With(x => x.Phone, requestModel.Phone)
                .With(x => x.FirstName, requestModel.FirstName)
                .With(x => x.LastName, requestModel.LastName)
                .With(x => x.UserRoleId, requestModel.UserRoleId)
                .Create();

            _mapperMock.Setup(mapper => mapper.Map<UserUpdateRequestDTO>(requestModel)).Returns(mappedUser);

            //Act
            var result = _userController.UpdateUser(userId, requestModel);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();

            _userServiceMock.Verify(service => service.Update(userId, mappedUser));
        }

        [Fact]
        public void DeleteUser_UserExist_ReturnsNoContent()
        {
            //Arrange
            var userId = _fixture.Create<int>();

            //Act
            var result = _userController.DeleteUser(userId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>();

            _userServiceMock.Verify(service => service.Delete(userId));
        }
    }
}