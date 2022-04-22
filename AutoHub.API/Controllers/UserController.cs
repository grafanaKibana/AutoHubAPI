using AutoHub.API.Models;
using AutoHub.API.Models.UserModels;
using AutoHub.BusinessLogic.DTOs.UserDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.BusinessLogic.Models;
using AutoHub.Domain.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoHub.API.Controllers;

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
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <param name="paginationParameters"></param>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <returns>Returns list of users.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllUsers([FromQuery] PaginationParameters paginationParameters)
    {
        var users = await _userService.GetAll(paginationParameters);
        var result = new UserResponse
        {
            Users = users,
            Paging = users.Any() ? new PagingInfo(users.Min(x => x.UserId), users.Max(x => x.UserId)) : null
        };

        return Ok(result);
    }

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    /// <param name="userId"></param>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">User not found.</response>
    /// <returns>Returns user.</returns>
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(UserResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var user = await _userService.GetById(userId);

        return Ok(user);
    }

    /// <summary>
    /// Updates user.
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
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserUpdateRequest model)
    {
        var mappedUser = _mapper.Map<UserUpdateRequestDTO>(model);
        await _userService.Update(userId, mappedUser);

        return NoContent();
    }

    /// <summary>
    /// Adds role to user.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roleId"></param>
    /// <response code="204">User role was updated successfully.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="404">User not found.</response>
    /// <response code="409">User already have specified role.</response>
    /// <response code="422">Invalid role ID.</response>
    /// <returns></returns>
    [HttpPatch("{userId}/AddToRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddRoleToUser(int userId, int roleId)
    {
        await _userService.AddToRole(userId, roleId);

        return NoContent();
    }

    /// <summary>
    /// Removes role from user.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="roleId"></param>
    /// <response code="204">User role was updated successfully.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="404">User not found or user not have specified role.</response>
    /// <response code="422">Invalid role ID.</response>
    /// <returns></returns>
    [HttpPatch("{userId}/RemoveFromRole")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RemoveRoleFromUserUser(int userId, int roleId)
    {
        await _userService.RemoveFromRole(userId, roleId);

        return NoContent();
    }

    /// <summary>
    /// Deletes user.
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
    public async Task<IActionResult> DeleteUser(int userId)
    {
        await _userService.Delete(userId);

        return NoContent();
    }
}