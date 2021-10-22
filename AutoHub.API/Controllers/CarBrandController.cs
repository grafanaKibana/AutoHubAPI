using System;
using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarBrandModels;
using AutoHub.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarBrandController : Controller
    {
        private readonly ICarBrandService _carBrandService;
        private readonly IMapper _mapper;

        public CarBrandController(ICarBrandService carBrandService, IMapper mapper)
        {
            _carBrandService = carBrandService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var carBrands = _carBrandService.GetAll();
                var mappedCarBrands = _mapper.Map<IEnumerable<CarBrandResponseModel>>(carBrands);
                return Ok(mappedCarBrands);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateCarBrand([FromBody] CarBrandCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedCarBrand = _mapper.Map<CarBrand>(model);
                _carBrandService.CreateCarBrand(mappedCarBrand);

                return Ok(mappedCarBrand);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}