using AutoHub.API.Models.UserModels;
using AutoHub.BusinessLogic.DTOs.UserDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AutoHub.API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthenticationController(IUserService userService, IMapper mapper) : ControllerBase
{
    private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));

    /// <summary>
    /// Log-in with credentials.
    /// </summary>
    /// <param name="model">User login request model.</param>
    /// <response code="200">Logged in successfully.</response>
    /// <response code="400">Invalid model.</response>
    /// <response code="404">User not found.</response>
    /// <returns>Returns user data and JWT Token.</returns>
    [HttpPost("Login")]
    [ProducesResponseType(typeof(UserLoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginRequest model)
    {
        await _userService.Logout();

        var mappedUser = mapper.Map<UserLoginRequestDTO>(model);
        var authModel = await _userService.Login(mappedUser);
        var mappedAuthModel = mapper.Map<UserLoginResponse>(authModel);

        return Ok(mappedAuthModel);
    }

    /// <summary>
    /// Register new user.
    /// </summary>
    /// <param name="model"></param>
    /// <response code="201">Successfully registered.</response>
    /// <response code="400">Invalid model.</response>
    /// <returns></returns>
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequest model)
    {
        var mappedUser = mapper.Map<UserRegisterRequestDTO>(model);
        await _userService.Register(mappedUser);

        return StatusCode((int)HttpStatusCode.Created);
    }

    /// <summary>
    /// Logout current user.
    /// </summary>
    /// <returns code="200">Successfully logged out.</returns>
    /// <response code="401">Unauthorized Access.</response>
    [HttpPost("Logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LogoutUser()
    {
        await _userService.Logout();

        return NoContent();
    }
}
