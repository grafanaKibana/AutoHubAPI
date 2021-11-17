using System;
using System.Collections.Generic;
using System.Net;
using AutoHub.API.Common;
using AutoHub.API.Models.UserModels;
using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        [HttpGet]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(typeof(IEnumerable<UserResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _userService.GetAll();
                var mappedUsers = _mapper.Map<IEnumerable<UserResponseModel>>(users);
                return Ok(mappedUsers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var user = _userService.GetById(userId);
                if (user == null)
                    return NotFound();

                var mappedUser = _mapper.Map<UserResponseModel>(user);
                return Ok(mappedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserLoginResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult LoginUser([FromBody] UserLoginRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var user = _userService.GetByEmail(model.Email);

                if (user == null)
                    return NotFound("User not found");

                var mappedUser = _mapper.Map<UserLoginRequestDTO>(model);
                var authModel = _userService.Login(mappedUser);

                if (authModel == null)
                    return BadRequest();

                var mappedAuthModel = _mapper.Map<UserLoginResponseModel>(authModel);

                return Ok(mappedAuthModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedUser = _mapper.Map<UserRegisterRequestDTO>(model);
                _userService.Register(mappedUser);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUser(int userId, [FromBody] UserUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                if (_userService.GetById(userId) == null)
                    return NotFound("User not found");

                if (!Enum.IsDefined(typeof(UserRoleEnum), model.UserRoleId))
                    return UnprocessableEntity("Incorrect user role ID");

                var mappedUser = _mapper.Map<UserUpdateRequestDTO>(model);

                _userService.Update(userId, mappedUser);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                if (_userService.GetById(userId) == null)
                    return NotFound();

                _userService.Delete(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}