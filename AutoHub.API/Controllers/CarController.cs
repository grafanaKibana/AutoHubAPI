using System;
using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarModels;
using AutoHub.DAL.Entities;
using AutoMapper;
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

                var mappedCar = _mapper.Map<Car>(model);
                _carService.CreateCar(mappedCar);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}