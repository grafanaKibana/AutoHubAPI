using AutoHub.API.Common;
using AutoHub.API.Models.UserModels;
using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAll();
            var mappedUsers = _mapper.Map<IEnumerable<UserResponseModel>>(users);

            return Ok(mappedUsers);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserById(int userId)
        {
            var user = _userService.GetById(userId);
            var mappedUser = _mapper.Map<UserResponseModel>(user);

            return Ok(mappedUser);
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserLoginResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult LoginUser([FromBody] UserLoginRequestModel model)
        {
            var mappedUser = _mapper.Map<UserLoginRequestDTO>(model);
            var authModel = _userService.Login(mappedUser);
            var mappedAuthModel = _mapper.Map<UserLoginResponseModel>(authModel);

            return Ok(mappedAuthModel);
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            var mappedUser = _mapper.Map<UserRegisterRequestDTO>(model);
            _userService.Register(mappedUser);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("{userId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUser(int userId, [FromBody] UserUpdateRequestModel model)
        {
            var mappedUser = _mapper.Map<UserUpdateRequestDTO>(model);
            _userService.Update(userId, mappedUser);

            return NoContent();
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteUser(int userId)
        {
            _userService.Delete(userId);

            return NoContent();
        }
    }
}