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
using System.Threading.Tasks;

namespace AutoHub.API.Controllers
{
    [ApiController]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
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
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <returns>Returns list of users.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            var mappedUsers = _mapper.Map<IEnumerable<UserResponseModel>>(users);

            return Ok(mappedUsers);
        }

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">User not found.</response>
        /// <returns>Returns user.</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UserResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserById(int userId)
        {
            var user = _userService.GetByIdAsync(userId);
            var mappedUser = _mapper.Map<UserResponseModel>(user);

            return Ok(mappedUser);
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
        ///         "phoneNumber": "+380000000000"
        ///     }
        /// 
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="model"></param>
        /// <response code="204">User was updated successfully.</response>
        /// <response code="400">Invalid model.</response>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">User not found.</response>
        /// <response code="422">Invalid user role ID.</response>
        /// <returns></returns>
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="404">User not found.</response>
        /// <response code="422">Invalid role ID.</response>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">User not found.</response>
        /// <returns></returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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