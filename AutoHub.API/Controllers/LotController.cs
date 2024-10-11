using AutoHub.API.Models.LotModels;
using AutoHub.BusinessLogic.DTOs.LotDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.Domain.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Linq;
using AutoHub.API.Models;
using AutoHub.BusinessLogic.Models;

namespace AutoHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]s")]
[Produces("application/json")]
public class LotController(ILotService lotService, IMapper mapper) : ControllerBase
{
    private readonly ILotService _lotService = lotService ?? throw new ArgumentNullException(nameof(lotService));

    /// <summary>
    /// Get all lots.
    /// </summary>
    /// <param name="paginationParameters">Pagination parameters model.</param>
    /// <response code="401">Unauthorized Access.</response>
    /// <returns>Returns list of lots.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(LotResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllLots([FromQuery] PaginationParameters paginationParameters)
    {
        var lots = (await _lotService.GetAll(paginationParameters)).ToList();
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
    /// <param name="paginationParameters">Pagination parameters model.</param>
    /// <response code="401">Unauthorized Access.</response>
    /// <returns>Returns list of lots with status "In progress".</returns>
    [HttpGet("Active")]
    [ProducesResponseType(typeof(LotResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetLotsInProgress([FromQuery] PaginationParameters paginationParameters)
    {
        var lots = (await _lotService.GetInProgress(paginationParameters)).ToList();
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
    /// <param name="lotId">Id of a lot.</param>
    /// <response code="401">Unauthorized Access.</response>
    /// <returns>Returns a lot.</returns>
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
    /// <param name="model">Lot create request model.</param>
    /// <response code="201">Lot was created successfully.</response>
    /// <response code="400">Invalid model.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin and Seller access only.</response>
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
        var mappedLot = mapper.Map<LotCreateRequestDTO>(model);
        await _lotService.Create(mappedLot);

        return StatusCode((int)HttpStatusCode.Created);
    }

    /// <summary>
    /// Update lot.
    /// </summary>
    /// <param name="lotId">Id of a lot.</param>
    /// <param name="model">Lot update request model.</param>
    /// <response code="204">Lot was updated successfully.</response>
    /// <response code="400">Invalid model.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Lot not found.</response>
    /// <response code="422">Invalid status ID.</response>
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
        var mappedLot = mapper.Map<LotUpdateRequestDTO>(model);
        await _lotService.Update(lotId, mappedLot);

        return NoContent();
    }

    /// <summary>
    /// Update lot status.
    /// </summary>
    /// <param name="lotId">Id of a lot.</param>
    /// <param name="statusId">Id of new lot status.</param>
    /// <response code="204">Lot status was updated successfully.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Lot not found.</response>
    /// <response code="422">Invalid status ID.</response>
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
    /// <param name="lotId">Id of a lot.</param>
    /// <response code="204">Lot was deleted successfully.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Lot not found.</response>
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
