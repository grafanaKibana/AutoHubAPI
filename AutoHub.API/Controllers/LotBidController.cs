using AutoHub.API.Models.BidModels;
using AutoHub.BusinessLogic.DTOs.BidDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.Domain.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoHub.API.Models;
using AutoHub.BusinessLogic.Models;

namespace AutoHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/Lots/{lotId}/Bids")]
[Produces("application/json")]
public class LotBidController(IBidService bidService, IMapper mapper) : ControllerBase
{
    private readonly IBidService _bidService = bidService ?? throw new ArgumentNullException(nameof(bidService));

    /// <summary>
    /// Get all bids of specific lot.
    /// </summary>
    /// <param name="lotId">Id of a lot bid.</param>
    /// <param name="paginationParameters">Pagination parameters model.</param>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Lot not found.</response>
    /// <returns>List of bids of lot.</returns>
    [HttpGet]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(typeof(BidResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLotBids(int lotId, [FromQuery] PaginationParameters paginationParameters)
    {
        var bids = (await _bidService.GetLotBids(lotId, paginationParameters)).ToList();
        var result = new BidResponse
        {
            Bids = bids,
            Paging = bids.Any() ? new PagingInfo(bids.Min(x => x.BidId), bids.Max(x => x.BidId)) : null
        };

        return Ok(result);
    }

    /// <summary>
    /// Create lot bid.
    /// </summary>
    /// <param name="lotId">Id of a lot bid.</param>
    /// <param name="model">Lot bid create request model.</param>
    /// <response code="201">Bid was created successfully.</response>
    /// <response code="400">Invalid model.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="404">Lot not found.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBid(int lotId, [FromBody] BidCreateRequest model)
    {
        var mappedBid = mapper.Map<BidCreateRequestDTO>(model);
        await _bidService.Create(lotId, mappedBid);

        return StatusCode((int)HttpStatusCode.Created);
    }
}
