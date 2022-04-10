using AutoHub.API.Common;
using AutoHub.API.Models.BidModels;
using AutoHub.BLL.DTOs.BidDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace AutoHub.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Lots/{lotId}/Bids")]
    [Produces("application/json")]
    public class LotBidController : Controller
    {
        private readonly IBidService _bidService;
        private readonly IMapper _mapper;

        public LotBidController(IBidService bidService, IMapper mapper)
        {
            _bidService = bidService ?? throw new ArgumentNullException(nameof(bidService));
            _mapper = mapper;
        }

        /// <summary>
        /// Get all bids of specific lot.
        /// </summary>
        /// <param name="lotId"></param>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">Lot not found.</response>
        /// <returns>List of bids of lot.</returns>
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

        /// <summary>
        /// Create lot bid.
        /// </summary>
        /// <param name="lotId"></param>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Lots/1
        ///     {
        ///         "userId": 1,
        ///         "bidValue": 50000
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Bid was created successfully</response>
        /// <response code="400">Invalid model</response>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="404">Lot not found</response>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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