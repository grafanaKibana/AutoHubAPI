using AutoHub.API.Models;
using AutoHub.API.Models.CarModelModels;
using AutoHub.BusinessLogic.DTOs.CarModelDTOs;
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
public class CarModelController : Controller
{
    private readonly ICarModelService _carModelService;
    private readonly IMapper _mapper;

    public CarModelController(ICarModelService carModelService, IMapper mapper)
    {
        _carModelService = carModelService ?? throw new ArgumentNullException(nameof(carModelService));
        _mapper = mapper;
    }

    /// <summary>
    /// Get all car models.
    /// </summary>
    /// <response code="401">Unauthorized Access.</response>
    /// <returns>Returns list of car models</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CarModelResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCarModels([FromQuery] PaginationParameters paginationParameters)
    {
        var carModels = await _carModelService.GetAll(paginationParameters);
        var result = new CarModelResponse
        {
            CarModels = carModels,
            Paging = carModels.Any() ? new PagingInfo(carModels.Min(x => x.CarModelId), carModels.Max(x => x.CarModelId)) : null,
        };
        return Ok(result);
    }

    /// <summary>
    /// Create car model.
    /// </summary>
    /// <param name="model"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /CarModels
    ///     {
    ///         "carModelName": "RS6 Avant"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Model was created successfully</response>
    /// <response code="400">Invalid model</response>
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
    public async Task<IActionResult> CreateCarModel([FromBody] CarModelCreateRequest model)
    {
        var mappedCarModel = _mapper.Map<CarModelCreateRequestDTO>(model);
        await _carModelService.Create(mappedCarModel);

        return StatusCode((int)HttpStatusCode.Created);
    }

    /// <summary>
    /// Updates car model
    /// </summary>
    /// <param name="carModelId"></param>
    /// <param name="model"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /CarModels
    ///     {
    ///         "carModelName": "RS6 Avant"
    ///     }
    ///
    /// </remarks>
    /// <response code="204">Model was updated successfully</response>
    /// <response code="400">Invalid model</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Model not found</response>
    /// <returns></returns>
    [HttpPut("{carModelId}")]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCarModel(int carModelId, [FromBody] CarModelUpdateRequest model)
    {
        var mappedCarModel = _mapper.Map<CarModelUpdateRequestDTO>(model);
        await _carModelService.Update(carModelId, mappedCarModel);

        return NoContent();
    }

    /// <summary>
    /// Deletes car model
    /// </summary>
    /// <param name="carModelId"></param>
    /// <response code="204">Model was deleted successfully</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Model not found</response>
    /// <returns></returns>
    [HttpDelete("{carModelId}")]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCarModel(int carModelId)
    {
        await _carModelService.Delete(carModelId);

        return NoContent();
    }
}