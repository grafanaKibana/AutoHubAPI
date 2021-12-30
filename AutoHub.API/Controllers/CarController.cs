using AutoHub.API.Models.CarModels;
using AutoHub.BLL.DTOs.CarDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using AutoHub.API.Common;
using Microsoft.AspNetCore.Authorization;

namespace AutoHub.API.Controllers
{
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
            _carService = carService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all cars.
        /// </summary>
        /// <returns>Returns list of cars</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCars()
        {
            var cars = _carService.GetAll();
            var mappedCars = _mapper.Map<IEnumerable<CarResponseModel>>(cars);

            return Ok(mappedCars);
        }

        /// <summary>
        /// Get a car by ID.
        /// </summary>
        /// <param name="carId"></param>
        /// <response code="404">Car not found</response>
        /// <returns>Returns car</returns>
        [HttpGet("{carId}")]
        [ProducesResponseType(typeof(IEnumerable<CarResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCarById(int carId)
        {
            var car = _carService.GetById(carId);
            var mappedCar = _mapper.Map<CarResponseModel>(car);

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
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = AuthorizationRoles.Seller)]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCar([FromBody] CarCreateRequestModel model)
        {
            var mappedCar = _mapper.Map<CarCreateRequestDTO>(model);
            _carService.Create(mappedCar);

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
        /// <response code="404">Car not found.</response>
        /// <response code="422">Invalid status ID.</response>
        /// <returns></returns>
        [HttpPut("{carId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCar(int carId, [FromBody] CarUpdateRequestModel model)
        {
            var mappedCar = _mapper.Map<CarUpdateRequestDTO>(model);

            _carService.Update(carId, mappedCar);
            return NoContent();
        }


        /// <summary>
        /// Update car status.
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="statusId"></param>
        /// <response code="204">Car status was updated successfully.</response>
        /// <response code="404">Car not found.</response>
        /// <response code="422">Invalid status ID.</response>
        /// <returns></returns>
        [HttpPatch]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCarStatus(int carId, int statusId)
        {
            _carService.UpdateStatus(carId, statusId);

            return NoContent();
        }

        /// <summary>
        /// Delete car.
        /// </summary>
        /// <param name="carId"></param>
        /// <response code="204">Car was deleted successfully.</response>
        /// <response code="404">Car not found.</response>
        /// <returns></returns>
        [HttpDelete("{carId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCar(int carId)
        { 
            _carService.Delete(carId); 
            return NoContent();
        }
    }
}