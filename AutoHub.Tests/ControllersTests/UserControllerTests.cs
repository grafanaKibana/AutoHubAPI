using System;
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
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GetById_UserExists_ReturnsOk()
        {
            //Arrange
            var user = _fixture.Create<UserResponseDTO>();
            _userServiceMock.Setup(service => service.GetById(It.IsAny<int>())).Returns(user);

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

        /*
        //Todo: Review this test (dont work as expected) 
        [Fact]
        public void LoginUser_ValidData_ReturnsOk()
        {
            //Arrange
            var requestModel = _fixture.Create<UserLoginRequestModel>();
            var requestDTO = _mapperMock.Object.Map<UserLoginRequestDTO>(requestModel);
            _userServiceMock.Setup(service => service.Login(requestDTO));
            
            //Act
            var result = _userController.LoginUser(requestModel);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
        }
        */

        [Fact]
        public void RegisterUser_ValidModel_ReturnsCreated()
        {
            //Arrange
            var requestModel = _fixture.Create<UserRegisterRequestModel>();
            var requestDTO = _mapperMock.Object.Map<UserRegisterRequestDTO>(requestModel);
            _userServiceMock.Setup(service => service.Register(requestDTO));

            //Act
            var result = _userController.RegisterUser(requestModel);

            //Assert
            result.Should()
                .BeOfType<StatusCodeResult>().And.BeEquivalentTo(new StatusCodeResult(201));
        }

        [Fact]
        public void RegisterUser_InvalidModel_ReturnsBadRequest()
        {
            //Arrange
            var requestModel = _fixture.Create<object>();
            _userServiceMock.Setup(service => service.Register(It.IsAny<object>() as UserRegisterRequestDTO));

            //Act
            var result = _userController.RegisterUser(requestModel as UserRegisterRequestModel);

            //Assert
            result.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public void UpdateUser_ValidModel_ReturnsNoContent()
        {
            //Arrange

            //Act

            //Assert
            throw new NotImplementedException();
        }

        [Fact]
        public void UpdateUser_InvalidModel_ReturnsBadReques()
        {
            //Arrange

            //Act

            //Assert
            throw new NotImplementedException();
        }

        [Fact]
        public void UpdateUser_UserNotExist_ReturnsNotFound()
        {
            //Arrange

            //Act

            //Assert
            throw new NotImplementedException();
        }

        [Fact]
        public void UpdateUser_IncorrectStatusId_ReturnsUnprocessableEntity()
        {
            //Arrange

            //Act

            //Assert
            throw new NotImplementedException();
        }

        [Fact]
        public void DeleteUser_UserExist_ReturnsNoContent()
        {
            //Arrange

            //Act

            //Assert
            throw new NotImplementedException();
        }

        [Fact]
        public void DeleteUser_UserNotExist_ReturnsNotFound()
        {
            //Arrange

            //Act

            //Assert
            throw new NotImplementedException();
        }
    }
}