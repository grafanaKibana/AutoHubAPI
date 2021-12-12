using AutoHub.API.Models.CarModels;
using AutoHub.BLL.DTOs.CarDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCars()
        {
            var cars = _carService.GetAll();
            var mappedCars = _mapper.Map<IEnumerable<CarResponseModel>>(cars);

            return Ok(mappedCars);
        }

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCar([FromBody] CarCreateRequestModel model)
        {
            var mappedCar = _mapper.Map<CarCreateRequestDTO>(model);
            _carService.Create(mappedCar);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("{carId}")]
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

        [HttpDelete("{carId}")]
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