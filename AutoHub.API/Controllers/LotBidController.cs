using AutoHub.API.Common;
using AutoHub.API.Models.BidModels;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace AutoHub.API.Controllers
{
    [Route("api/Lots/{lotId}/Bids")]
    [ApiController]
    public class LotBidController : Controller
    {
        private readonly IBidService _bidService;
        private readonly IMapper _mapper;

        public LotBidController(IBidService bidService, IMapper mapper)
        {
            _bidService = bidService;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(typeof(IEnumerable<BidResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetLotBids(int lotId)
        {
            var bids = _bidService.GetLotBids(lotId);
            var mappedBids = _mapper.Map<IEnumerable<BidResponseModel>>(bids);

            return Ok(mappedBids);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateBid(int lotId, [FromBody] BidCreateRequestModel model)
        {
            var mappedBid = _mapper.Map<BidCreateRequestDTO>(model);
            _bidService.Create(lotId, mappedBid);

            return StatusCode((int)HttpStatusCode.Created);
        }
    }
}