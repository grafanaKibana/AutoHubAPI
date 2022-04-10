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
        public async void GetAllUsers_ReturnsOk()
        {
            //Arrange
            var users = _fixture.CreateMany<UserResponseDTO>();
            var mappedUsers = users.Select(userDTO => _fixture.Build<UserResponseModel>()
                .With(x => x.Email, userDTO.Email)
                .With(x => x.PhoneNumber, userDTO.PhoneNumber)
                .With(x => x.FirstName, userDTO.FirstName)
                .With(x => x.LastName, userDTO.LastName)
                .With(x => x.UserId, userDTO.UserId)
                .With(x => x.UserRoles, userDTO.UserRoles)
                .With(x => x.RegistrationTime, userDTO.RegistrationTime)
                .Create());

            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserResponseModel>>(users)).Returns(mappedUsers);
            _userServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(users);

            //Act
            var result = await _userController.GetAllUsers();

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
                .With(x => x.PhoneNumber, user.PhoneNumber)
                .With(x => x.FirstName, user.FirstName)
                .With(x => x.LastName, user.LastName)
                .With(x => x.UserId, user.UserId)
                .With(x => x.UserRoles, user.UserRoles)
                .With(x => x.RegistrationTime, user.RegistrationTime)
                .Create();

            _mapperMock.Setup(mapper => mapper.Map<UserResponseModel>(user)).Returns(mappedUser);
            _userServiceMock.Setup(service => service.GetByIdAsync(user.UserId)).ReturnsAsync(user);

            //Act
            var result = _userController.GetUserById(user.UserId);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void UpdateUser_ValidData_ReturnsNoContent()
        {
            //Arrange
            var userId = _fixture.Create<int>();
            var requestModel = _fixture.Create<UserUpdateRequestModel>();

            var mappedUser = _fixture.Build<UserUpdateRequestDTO>()
                .With(x => x.Email, requestModel.Email)
                .With(x => x.PhoneNumber, requestModel.PhoneNumber)
                .With(x => x.FirstName, requestModel.FirstName)
                .With(x => x.LastName, requestModel.LastName)
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