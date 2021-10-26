using System;
using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.UserModels;
using AutoHub.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPut("SetAdminRole")]
        public IActionResult SetAdminRole(int userId)
        {
            try
            {
                var success = _userService.SetAdminRole(userId);
                return success ? Ok(success) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("SetRegularRole")]
        public IActionResult SetRegularRole(int userId)
        {
            try
            {
                var success = _userService.SetRegularRole(userId);
                return success ? Ok(success) : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}