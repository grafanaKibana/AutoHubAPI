using AutoHub.API.Common;
using AutoHub.API.Models.BidModels;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AutoHub.API.Controllers
{
    [Route("api/Users/{userId}/Bids")]
    [ApiController]
    public class UserBidController : Controller
    {
        private readonly IBidService _bidService;
        private readonly IMapper _mapper;

        public UserBidController(IBidService bidService, IMapper mapper)
        {
            _bidService = bidService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(typeof(IEnumerable<BidResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetUserBids(int userId)
        {
            var bids = _bidService.GetUserBids(userId);
            var mappedBids = _mapper.Map<IEnumerable<BidResponseModel>>(bids);

            return Ok(mappedBids);
        }
    }
}