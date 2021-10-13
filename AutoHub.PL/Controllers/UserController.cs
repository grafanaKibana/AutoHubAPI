using System;
using AutoHub.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_userService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}