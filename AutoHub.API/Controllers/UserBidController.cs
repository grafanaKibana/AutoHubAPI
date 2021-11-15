using System;
using System.Collections.Generic;
using AutoHub.API.Models.BidModels;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/Users/{userId}/Bids")]
    [ApiController]
    public class UserBidController : Controller
    {
        private readonly IBidService _bidService;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserBidController(IBidService bidService, IUserService userService, IMapper mapper)
        {
            _bidService = bidService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BidResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetUserBids(int userId)
        {
            try
            {
                var user = _userService.GetById(userId);

                if (user == null)
                    return NotFound();

                var bids = _bidService.GetUserBids(userId);

                var mappedBids = _mapper.Map<IEnumerable<BidResponseModel>>(bids);
                return Ok(mappedBids);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}