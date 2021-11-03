using System;
using System.Collections.Generic;
using System.Net;
using AutoHub.API.Models.BidModels;
using AutoHub.API.Models.UserModels;
using AutoHub.BLL.DTOs.UserDTOs;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Enums;
using AutoMapper;
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
        [ProducesResponseType(typeof(IEnumerable<UserResponseModel>), StatusCodes.Status200OK)]
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

        [HttpGet("{userId}/Bids")]
        [ProducesResponseType(typeof(IEnumerable<BidResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetUserBids(int userId)
        {
            try
            {
                if (_userService.GetById(userId) == null)
                    return NotFound();

                var bids = _userService.GetBids(userId);
                var mappedBids = _mapper.Map<IEnumerable<BidResponseModel>>(bids);
                return Ok(mappedBids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("Login")]
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
        public IActionResult UpdateUser(int userId, [FromBody] UserUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                if (_userService.GetById(userId) == null)
                    return NotFound("User not found");

                if (!Enum.IsDefined(typeof(UserRoleEnum), model.UserRoleId))
                    return NotFound("Incorrect user role ID");

                var mappedUser = _mapper.Map<UserUpdateRequestDTO>(model);

                _userService.UpdateUser(userId, mappedUser);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}