using AutoHub.API.Models.CarColorModels;
using AutoHub.BusinessLogic.DTOs.CarColorDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.Domain.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AutoHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]s")]
[Produces("application/json")]
public class CarColorController : Controller
{
    private readonly ICarColorService _carColorService;
    private readonly IMapper _mapper;

    public CarColorController(ICarColorService carColorService, IMapper mapper)
    {
        _carColorService = carColorService ?? throw new ArgumentNullException(nameof(carColorService));
        _mapper = mapper;
    }

    /// <summary>
    /// Get all car colors.
    /// </summary>
    /// <response code="401">Unauthorized Access.</response>
    /// <returns>Returns list of car colors.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CarColorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCarColors()
    {
        var carColors = await _carColorService.GetAll();
        var mappedCarColors = _mapper.Map<IEnumerable<CarColorResponse>>(carColors);

        return Ok(mappedCarColors);
    }

    /// <summary>
    /// Create car color.
    /// </summary>
    /// <param name="model"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /CarColors
    ///     {
    ///         "carColorName": "Magenta"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Color was created successfully.</response>
    /// <response code="400">Invalid model.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCarColor([FromBody] CarColorCreateRequest model)
    {
        var mappedCarColor = _mapper.Map<CarColorCreateRequestDTO>(model);
        await _carColorService.Create(mappedCarColor);

        return StatusCode((int)HttpStatusCode.Created);
    }

    /// <summary>
    /// Update car color.
    /// </summary>
    /// <param name="carColorId"></param>
    /// <param name="model"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /CarColors
    ///     {
    ///         "carColorName": "Magenta"
    ///     }
    ///
    /// </remarks>
    /// <response code="204">Color was updated successfully.</response>
    /// <response code="400">Invalid model.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Color not found.</response>
    /// <returns></returns>
    [HttpPut("{carColorId}")]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCarColor(int carColorId, [FromBody] CarColorUpdateRequest model)
    {
        var mappedCarColor = _mapper.Map<CarColorUpdateRequestDTO>(model);
        await _carColorService.Update(carColorId, mappedCarColor);

        return NoContent();
    }

    /// <summary>
    /// Delete car color.
    /// </summary>
    /// <param name="carColorId"></param>
    /// <response code="204">Color was deleted successfully.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Color not found.</response>
    /// <returns></returns>
    [HttpDelete("{carColorId}")]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCarColor(int carColorId)
    {
        await _carColorService.Delete(carColorId);

        return NoContent();
    }
}
