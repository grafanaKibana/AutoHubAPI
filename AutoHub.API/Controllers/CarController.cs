using AutoHub.API.Models;
using AutoHub.API.Models.CarModels;
using AutoHub.BusinessLogic.DTOs.CarDTOs;
using AutoHub.BusinessLogic.Interfaces;
using AutoHub.BusinessLogic.Models;
using AutoHub.Domain.Constants;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AutoHub.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]s")]
[Produces("application/json")]
public class CarController : Controller
{
    private readonly ICarService _carService;
    private readonly IMapper _mapper;

    public CarController(ICarService carService, IMapper mapper)
    {
        _carService = carService ?? throw new ArgumentNullException(nameof(carService));
        _mapper = mapper;
    }

    /// <summary>
    /// Get all cars.
    /// </summary>
    /// <response code="401">Unauthorized Access.</response>
    /// <returns>Returns list of cars.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CarResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllCars([FromQuery] PaginationParameters paginationParameters)
    {
        var cars = await _carService.GetAll(paginationParameters);
        var result = new CarResponse
        {
            Cars = cars,
            Paging = cars.Any() ? new PagingInfo(cars.Min(x => x.CarId), cars.Max(x => x.CarId)) : null,
        };

        return Ok(result);
    }

    /// <summary>
    /// Get a car by ID.
    /// </summary>
    /// <param name="carId"></param>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="404">Car not found</response>
    /// <returns>Returns car</returns>
    [HttpGet("{carId}")]
    [ProducesResponseType(typeof(IEnumerable<CarResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCarById(int carId)
    {
        var car = await _carService.GetById(carId);
        var mappedCar = _mapper.Map<CarResponse>(car);

        return Ok(mappedCar);
    }

    /// <summary>
    /// Create car.
    /// </summary>
    /// <param name="model"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Cars
    ///     {
    ///         "carBrand": "Audi",
    ///         "carModel": "RS6 Avant",
    ///         "carColor": "Magenta",
    ///         "imgUrl": "shorturl.at/oyCFN",
    ///         "description": "Audi endows the RS6 Avant with a twin-turbocharged 4.0-liter V-8, which generates 591 horsepower and 590 pound-feet of torque",
    ///         "year": 2021,
    ///         "vin": "WAUFMBFC0GN059183",
    ///         "mileage": 15550,
    ///         "sellingPrice": 137000,
    ///         "costPrice": 129500
    ///     }
    ///
    /// </remarks>
    /// <response code="201">Car was created successfully.</response>
    /// <response code="400">Invalid model.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin or Seller access only.</response>
    /// <returns></returns>
    [HttpPost]
    [Authorize(Roles = AuthorizationRoles.Seller)]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCar([FromBody] CarCreateRequest model)
    {
        var mappedCar = _mapper.Map<CarCreateRequestDTO>(model);
        await _carService.Create(mappedCar);

        return StatusCode((int)HttpStatusCode.Created);
    }

    /// <summary>
    /// Update car.
    /// </summary>
    /// <param name="carId"></param>
    /// <param name="model"></param>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT /Cars/1
    ///     {
    ///         "carBrand": "Audi",
    ///         "carModel": "RS6 Avant",
    ///         "carColor": "Magenta",
    ///         "imgUrl": "shorturl.at/oyCFN",
    ///         "description": "Audi endows the RS6 Avant with a twin-turbocharged 4.0-liter V-8, which generates 591 horsepower and 590 pound-feet of torque",
    ///         "year": 2021,
    ///         "vin": "WAUFMBFC0GN059183",
    ///         "mileage": 15550,
    ///         "sellingPrice": 137000,
    ///         "costPrice": 129500,
    ///         "carStatusId": 2
    ///     }
    ///
    /// </remarks>
    /// <response code="204">Car was updated successfully.</response>
    /// <response code="400">Invalid model.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Car not found.</response>
    /// <response code="422">Invalid status ID.</response>
    /// <returns></returns>
    [HttpPut("{carId}")]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCar(int carId, [FromBody] CarUpdateRequest model)
    {
        var mappedCar = _mapper.Map<CarUpdateRequestDTO>(model);
        await _carService.Update(carId, mappedCar);

        return NoContent();
    }

    /// <summary>
    /// Update car status.
    /// </summary>
    /// <param name="carId"></param>
    /// <param name="statusId"></param>
    /// <response code="204">Car status was updated successfully.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Car not found.</response>
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
    public async Task<IActionResult> UpdateCarStatus(int carId, int statusId)
    {
        await _carService.UpdateStatus(carId, statusId);

        return NoContent();
    }

    /// <summary>
    /// Delete car.
    /// </summary>
    /// <param name="carId"></param>
    /// <response code="204">Car was deleted successfully.</response>
    /// <response code="401">Unauthorized Access.</response>
    /// <response code="403">Admin access only.</response>
    /// <response code="404">Car not found.</response>
    /// <returns></returns>
    [HttpDelete("{carId}")]
    [Authorize(Roles = AuthorizationRoles.Administrator)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCar(int carId)
    {
        await _carService.Delete(carId);

        return NoContent();
    }
}