using AutoHub.API.Models;
using AutoHub.API.Models.CarBrandModels;
using AutoHub.BusinessLogic.DTOs.CarBrandDTOs;
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
public class CarBrandController : Controller
{
    private readonly ICarBrandService _carBrandService;
    private readonly IMapper _mapper;

    public CarBrandController(ICarBrandService carBrandService, IMapper mapper)
    {
        _carBrandService = carBrandService ?? throw new ArgumentNullException(nameof(carBrandService));
        _mapper = mapper;
    }

    /// <summary>
    /// Get all car brands.
    /// </summary>
    /// <response code="401">Unauthorized Access.</response>
    /// <returns>Returns list of car brands</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CarBrandResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCarBrands([FromQuery] PaginationParameters paginationParameters)
    {
        var carBrands = await _carBrandService.GetAll(paginationParameters);
        var result = new CarBrandResponse
        {
            CarBrands = carBrands,
            Paging = carBrands.Any() ? new PagingInfo(carBrands.Min(x => x.CarBrandId), carBrands.Max(x => x.CarBrandId)) : null,
        };
        return Ok(result);
    }

    /// <summary>
    /// Create car brand.
    /// </summary>
    /// <param name="model"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /CarBrands
    ///     {
    ///         "carBrandName": "Audi"
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Brand was created successfully</response>
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
    public async Task<IActionResult> CreateCarBrand([FromBody] CarBrandCreateRequest model)
    {
        var mappedCarBrand = _mapper.Map<CarBrandCreateRequestDTO>(model);
        await _carBrandService.Create(mappedCarBrand);

        return StatusCode((int)HttpStatusCode.Created);
    }

    /// <summary>
    /// Update car brand.
    /// </summary>
    /// <param name="carBrandId"></param>
    /// <param name="model"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /CarBrands
    ///     {
    ///         "carBrandName": "Audi"
    ///     }
    ///
    /// </remarks>
    /// <response code="204">Brand was updated successfully</response>
    /// <response code="400">Invalid model</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Brand not found</response>
    /// <returns></returns>
    [HttpPut("{carBrandId}")]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCarBrand(int carBrandId, [FromBody] CarBrandUpdateRequest model)
    {
        var mappedCarBrand = _mapper.Map<CarBrandUpdateRequestDTO>(model);
        await _carBrandService.Update(carBrandId, mappedCarBrand);

        return NoContent();
    }

    /// <summary>
    /// Delete car brand.
    /// </summary>
    /// <param name="carBrandId"></param>
    /// <response code="204">Brand was deleted successfully</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Brand not found</response>
    /// <returns></returns>
    [HttpDelete("{carBrandId}")]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCarBrand(int carBrandId)
    {
        await _carBrandService.Delete(carBrandId);

        return NoContent();
    }
}