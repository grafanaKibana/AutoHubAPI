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

        public UserBidController(IBidService userService, IMapper mapper)
        {
            _bidService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BidResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetUserBids(int userId)
        {
            try
            {
                var bids = _bidService.GetUserBids(userId);
                if (bids == null)
                    return NotFound();

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