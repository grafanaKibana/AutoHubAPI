using System;
using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarBrandModels;
using AutoHub.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]s")]
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
        [ProducesResponseType(typeof(IEnumerable<CarBrandResponseModel>), StatusCodes.Status200OK)]
        public IActionResult CreateCarBrand([FromBody] CarBrandCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedCarBrand = _mapper.Map<CarBrand>(model);
                _carBrandService.CreateCarBrand(mappedCarBrand);

                return StatusCode(201, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{carBrandId}")]
        public IActionResult UpdateCarBrand(int carBrandId, [FromBody] CarBrandUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                if (_carBrandService.GetById(carBrandId) == null)
                    return NotFound();

                var mappedCarBrand = _mapper.Map<CarBrand>(model);
                _carBrandService.UpdateCarBrand(mappedCarBrand);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}