using System;
using System.Collections.Generic;
using System.Net;
using AutoHub.API.Models.CarModels;
using AutoHub.BLL.DTOs.CarDTOs;
using AutoHub.BLL.Interfaces;
using AutoHub.DAL.Enums;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllCars()
        {
            try
            {
                var cars = _carService.GetAll();
                var mappedCars = _mapper.Map<IEnumerable<CarResponseModel>>(cars);
                return Ok(mappedCars);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{carId}")]
        [ProducesResponseType(typeof(IEnumerable<CarResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetCarById(int carId)
        {
            try
            {
                var car = _carService.GetById(carId);
                if (car == null)
                    return NotFound();
                var mappedCar = _mapper.Map<CarResponseModel>(car);
                return Ok(mappedCar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateCar([FromBody] CarCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedCar = _mapper.Map<CarCreateRequestDTO>(model);
                _carService.Create(mappedCar);

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{carId}")]
        public IActionResult UpdateCar(int carId, [FromBody] CarUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                if (_carService.GetById(carId) == null)
                    return NotFound();
                if (!Enum.IsDefined(typeof(CarStatusEnum), model.CarStatusId))
                    return UnprocessableEntity("Incorrect car status ID");

                var mappedCar = _mapper.Map<CarUpdateRequestDTO>(model);

                _carService.Update(carId, mappedCar);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{carId}")]
        public IActionResult DeleteCar(int carId)
        {
            try
            {
                if (_carService.GetById(carId) == null)
                    return NotFound();
                _carService.Delete(carId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}