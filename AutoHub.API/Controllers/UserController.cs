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
    [ApiController]
    [Route("api/[controller]s")]
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <response code="403">Admin access only.</response>
        /// <returns>Returns list of users</returns>
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

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">User not found.</response>
        /// <returns>Returns user.</returns>
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

        /// <summary>
        /// Log-in with credentials.
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /User/Login
        ///     {
        ///         "email": "user@mail.com",
        ///         "password": "PasSw0Rd666"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Logged in successfully.</response>
        /// <response code="400">Invalid model.</response>
        /// <response code="404">User not found.</response>
        /// <returns>Returns E-mail and JWT Token.</returns>
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

        /// <summary>
        /// Register new user.
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /User/Register
        ///     {
        ///         "firstName": "John",
        ///         "lastName": "Walker",
        ///         "email": "user@mail.com",
        ///         "phone": "+380000000000",
        ///         "password": "PasSw0Rd666"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully registered.</response>
        /// <response code="400">Invalid model.</response>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            var mappedUser = _mapper.Map<UserRegisterRequestDTO>(model);
            _userService.Register(mappedUser);

            return StatusCode((int) HttpStatusCode.Created);
        }

        /// <summary>
        /// Update user.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///     PUT /User
        ///     {
        ///         "userRoleId": 3
        ///         "firstName": "John",
        ///         "lastName": "Walker",
        ///         "email": "user@mail.com",
        ///         "phone": "+380000000000",
        ///         "password": "PasSw0Rd666"
        ///     }
        /// 
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <response code="204">User was updated successfully.</response>
        /// <response code="400">Invalid model.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="422">Invalid user role ID.</response>
        /// <response code="404">User not found.</response>
        /// <returns></returns>
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

        /// <summary>
        /// Update user role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <response code="204">User role was updated successfully.</response>
        /// <response code="404">User not found.</response>
        /// <response code="422">Invalid role ID.</response>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUserRole(int userId, int roleId)
        {
            _userService.UpdateRole(userId, roleId);

            return NoContent();
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="204">User was deleted successfully.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">User not found.</response>
        /// <returns></returns>
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