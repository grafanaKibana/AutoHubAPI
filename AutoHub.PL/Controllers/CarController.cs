using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models;

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
        [Produces(typeof(List<CarModel>))]
        public IActionResult GetAllCars()
        {
            return Ok(_carService.GetAll());
        }
    }
}