using System;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.PL.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CarController : Controller
    {
        private readonly ICarService _carService;


        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public IActionResult GetAllCars()
        {
            try
            {
                return Ok(_carService.GetAll());
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
                return Ok(car);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        
        [HttpPost]
        public IActionResult AddCar([FromBody]CarModel carModel)
        {
            try
            {
                if (carModel == null)
                    return BadRequest();
                
                return StatusCode(201, _carService.CreateCar(carModel));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}