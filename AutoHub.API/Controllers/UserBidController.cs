using AutoHub.API.Models.BidModels;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.Domain.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoHub.API.Controllers;

[ApiController]
[Authorize(Roles = AuthorizationRoles.Administrator)]
[Route("api/Users/{userId}/Bids")]
[Produces("application/json")]
public class UserBidController : Controller
{
    private readonly IBidService _bidService;
    private readonly IMapper _mapper;

    public UserBidController(IBidService bidService, IMapper mapper)
    {
        _bidService = bidService ?? throw new ArgumentNullException(nameof(bidService));
        _mapper = mapper;
    }

    /// <summary>
    /// Returns all bids created by user
    /// </summary>
    /// <param name="userId"></param>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only</response>
    /// <response code="404">User not found</response>
    /// <returns>List of bids of user</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BidResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserBids(int userId)
    {
        var bids = await _bidService.GetUserBids(userId);
        var mappedBids = _mapper.Map<IEnumerable<BidResponse>>(bids);

        return Ok(mappedBids);
    }
}
