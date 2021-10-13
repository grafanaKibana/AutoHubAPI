using System;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarModels;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.PL.Controllers
{
    [Route("api/[controller]")]
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
        public IActionResult AddCar([FromBody] CarCreateRequestModel carCreateRequestModel)
        {
            try
            {
                if (carCreateRequestModel == null)
                    return BadRequest();

                _carService.CreateCar(carCreateRequestModel);

                return CreatedAtAction(nameof(GetCarById), new { id = carCreateRequestModel.CarId },
                    carCreateRequestModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /*[HttpPut("{id}")]
        public IActionResult UpdateCar(int id, [FromBody] CarUpdateRequestModel carUpdateApiModel)
        {
            try
            {
                if (_carService.GetById(id) == null)
                    return NotFound();
                
                if (carUpdateApiModel == null || id != carUpdateApiModel.CarId)
                    return BadRequest();

                _carService.UpdateCar(id, carUpdateApiModel);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }*/
    }
}