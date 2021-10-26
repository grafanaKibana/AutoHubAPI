using System;
using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.BidModels;
using AutoHub.BLL.Models.UserModels;
using AutoHub.DAL.Entities;
using AutoHub.DAL.Enums;
using AutoMapper;
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
        public IActionResult GetUserBids(int id)
        {
            try
            {
                if (_userService.GetById(id) == null)
                    return NotFound();

                var bids = _userService.GetBids(id);
                var mappedBids = _mapper.Map<IEnumerable<BidResponseModel>>(bids);
                return Ok(mappedBids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                var mappedUser = _mapper.Map<User>(model);
                var successfulRegistration = _userService.Register(mappedUser);
                if (!successfulRegistration)
                    return BadRequest();

                return StatusCode(201, model);
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
                    return NotFound();

                if (!Enum.IsDefined(typeof(UserRoleEnum), model.UserRoleId))
                    return NotFound("Incorrect user role ID");

                var mappedUser = _mapper.Map<User>(model);
                _userService.UpdateUser(mappedUser);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}