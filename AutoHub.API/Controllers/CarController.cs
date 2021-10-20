using System;
using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarModels;
using AutoHub.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            try
            {
                var car = _carService.GetById(id);
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
        public IActionResult CreateCar([FromBody] CarCreateRequestModel carCreateRequestModel)
        {
            try
            {
                if (carCreateRequestModel == null)
                    return BadRequest();

                var mappedCar = _mapper.Map<Car>(carCreateRequestModel);
                _carService.CreateCar(mappedCar);

                return Ok(mappedCar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}