using AutoFixture;
using AutoHub.API.Controllers;
using AutoHub.API.Models.UserModels;
using AutoHub.BusinessLogic.DTOs.UserDTOs;
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
    public async Task GetAllUsers_ReturnsOkAsync()
    {
        //Arrange
        var users = _fixture.CreateMany<UserResponseDTO>();
        var mappedUsers = users.Select(userDTO => _fixture.Build<UserResponse>()
            .With(x => x.Email, userDTO.Email)
            .With(x => x.PhoneNumber, userDTO.PhoneNumber)
            .With(x => x.FirstName, userDTO.FirstName)
            .With(x => x.LastName, userDTO.LastName)
            .With(x => x.UserId, userDTO.UserId)
            .With(x => x.UserRoles, userDTO.UserRoles)
            .With(x => x.RegistrationTime, userDTO.RegistrationTime)
            .Create());

        _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserResponse>>(users)).Returns(mappedUsers);
        _userServiceMock.Setup(service => service.GetAll()).ReturnsAsync(users);

        //Act
        var result = await _userController.GetAllUsers();

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task GetByUserById_UserExists_ReturnsOkAsync()
    {
        //Arrange
        var user = _fixture.Create<UserResponseDTO>();
        var mappedUser = _fixture.Build<UserResponse>()
            .With(x => x.Email, user.Email)
            .With(x => x.PhoneNumber, user.PhoneNumber)
            .With(x => x.FirstName, user.FirstName)
            .With(x => x.LastName, user.LastName)
            .With(x => x.UserId, user.UserId)
            .With(x => x.UserRoles, user.UserRoles)
            .With(x => x.RegistrationTime, user.RegistrationTime)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<UserResponse>(user)).Returns(mappedUser);
        _userServiceMock.Setup(service => service.GetById(user.UserId)).ReturnsAsync(user);

        //Act
        var result = await _userController.GetUserById(user.UserId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task UpdateUser_ValidData_ReturnsNoContentAsync()
    {
        //Arrange
        var userId = _fixture.Create<int>();
        var requestModel = _fixture.Create<UserUpdateRequest>();

        var mappedUser = _fixture.Build<UserUpdateRequestDTO>()
            .With(x => x.Email, requestModel.Email)
            .With(x => x.PhoneNumber, requestModel.PhoneNumber)
            .With(x => x.FirstName, requestModel.FirstName)
            .With(x => x.LastName, requestModel.LastName)
            .Create();

        _mapperMock.Setup(mapper => mapper.Map<UserUpdateRequestDTO>(requestModel)).Returns(mappedUser);

        //Act
        var result = await _userController.UpdateUser(userId, requestModel);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _userServiceMock.Verify(service => service.Update(userId, mappedUser));
    }

    [Fact]
    public async Task DeleteUser_UserExist_ReturnsNoContentAsync()
    {
        //Arrange
        var userId = _fixture.Create<int>();

        //Act
        var result = await _userController.DeleteUser(userId);

        //Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NoContentResult>();

        _userServiceMock.Verify(service => service.Delete(userId));
    }
}