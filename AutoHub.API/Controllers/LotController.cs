using AutoHub.API.Models;
using AutoHub.API.Models.LotModels;
using AutoHub.BusinessLogic.DTOs.LotDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.BusinessLogic.Models;
using AutoHub.Domain.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AutoHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]s")]
[Produces("application/json")]
public class LotController : Controller
{
    private readonly ILotService _lotService;
    private readonly IMapper _mapper;

    public LotController(ILotService lotService, IMapper mapper)
    {
        _lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));
        _mapper = mapper;
    }

    /// <summary>
    /// Get all lots.
    /// </summary>
    /// <response code="401">Unauthorized Access.</response>
    /// <returns>Returns list of lots.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(LotResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllLots([FromQuery] PaginationParameters paginationParameters)
    {
        var lots = await _lotService.GetAll(paginationParameters);
        var result = new LotResponse
        {
            Lots = lots,
            Paging = lots.Any() ? new PagingInfo(lots.Min(x => x.LotId), lots.Max(x => x.LotId)) : null,
        };

        return Ok(result);
    }

    /// <summary>
    /// Get all lots with status "In progress".
    /// </summary>
    /// <response code="401">Unauthorized Access.</response>
    /// <returns>Returns list of lots with status "In progress".</returns>
    [HttpGet("Active")]
    [ProducesResponseType(typeof(LotResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLotsInProgress([FromQuery] PaginationParameters paginationParameters)
    {
        var lots = await _lotService.GetInProgress(paginationParameters);
        var result = new LotResponse
        {
            Lots = lots,
            Paging = lots.Any() ? new PagingInfo(lots.Min(x => x.LotId), lots.Max(x => x.LotId)) : null,
        };

        return Ok(result);
    }

    /// <summary>
    /// Get a lot by ID.
    /// </summary>
    /// <param name="lotId"></param>
    /// <response code="401">Unauthorized Access.</response>
    /// <returns>Returns lot</returns>
    [HttpGet("{lotId}")]
    [ProducesResponseType(typeof(LotResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLotById(int lotId)
    {
        var lot = await _lotService.GetById(lotId);

        return Ok(lot);
    }

    /// <summary>
    /// Create lot.
    /// </summary>
    /// <param name="model"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Lots
    ///     {
    ///         "creatorId": 1,
    ///         "carId": 1,
    ///         "durationInDays": 7
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Lot was created successfully.</response>
    /// <response code="400">Invalid model.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin and Seller access only.</response>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = AuthorizationRoles.Seller)]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateLot([FromBody] LotCreateRequest model)
    {
        var mappedLot = _mapper.Map<LotCreateRequestDTO>(model);
        await _lotService.Create(mappedLot);

        return StatusCode((int)HttpStatusCode.Created);
    }

    /// <summary>
    /// Update lot.
    /// </summary>
    /// <param name="lotId"></param>
    /// <param name="model"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /Lots/1
    ///     {
    ///         "lotStatusId": 3,
    ///         "winnerId": 1,
    ///         "durationInDays": 7
    ///     }
    ///
    /// </remarks>
    /// <response code="204">Lot was updated successfully.</response>
    /// <response code="400">Invalid model.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Lot not found.</response>
    /// <response code="422">Invalid status ID.</response>
    /// <returns></returns>
    [HttpPut("{lotId}")]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateLot(int lotId, [FromBody] LotUpdateRequest model)
    {
        var mappedLot = _mapper.Map<LotUpdateRequestDTO>(model);
        await _lotService.Update(lotId, mappedLot);

        return NoContent();
    }

    /// <summary>
    /// Update lot status.
    /// </summary>
    /// <param name="lotId"></param>
    /// <param name="statusId"></param>
    /// <response code="204">Lot status was updated successfully.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Lot not found.</response>
    /// <response code="422">Invalid status ID.</response>
    /// <returns></returns>
    [HttpPatch]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateLotStatus(int lotId, int statusId)
    {
        await _lotService.UpdateStatus(lotId, statusId);

        return NoContent();
    }

    /// <summary>
    /// Delete lot.
    /// </summary>
    /// <param name="lotId"></param>
    /// <response code="204">Lot was deleted successfully.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Lot not found.</response>
    /// <returns></returns>
    [HttpDelete("{lotId}")]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteLot(int lotId)
    {
        await _lotService.Delete(lotId);

        return NoContent();
    }
}