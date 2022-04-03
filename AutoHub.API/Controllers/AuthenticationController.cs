using System;
using System.Net;
using System.Threading.Tasks;
using AutoHub.API.Models.UserModels;
using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthenticationController : Controller
    {
        private IMapper _mapper;
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper;
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
        ///         "username": "nightWalker",
        ///         "password": "PasSw0Rd666",
        ///         "rememberMe": true
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Logged in successfully.</response>
        /// <response code="400">Invalid model.</response>
        /// <response code="404">User not found.</response>
        /// <returns>Returns user data and JWT Token.</returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(UserLoginResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestModel model)
        {
            await _userService.LogoutAsync();
            var mappedUser = _mapper.Map<UserLoginRequestDTO>(model);
            var authModel = await _userService.LoginAsync(mappedUser);
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
        ///         "username": "nightWalker",
        ///         "email": "user@mail.com",
        ///         "phoneNumber": "+380000000000",
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
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            var mappedUser = _mapper.Map<UserRegisterRequestDTO>(model);
            await _userService.RegisterAsync(mappedUser);

            return StatusCode((int) HttpStatusCode.Created);
        }

        /// <summary>
        /// Logout current user.
        /// </summary>
        /// <returns code="200">Successfully logged out.</returns>
        /// <response code="401">Unauthorized Access.</response>
        [HttpPost("Logout")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task LogoutUser()
        {
            await _userService.LogoutAsync();
        }
    }
}