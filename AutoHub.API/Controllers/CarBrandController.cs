using System.Collections.Generic;
using System.Net;
using AutoHub.API.Models.CarBrandModels;
using AutoHub.BLL.DTOs.CarBrandDTOs;
using AutoHub.BLL.Interfaces;
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
        [ProducesResponseType(typeof(IEnumerable<CarBrandResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCarBrands()
        {
            var carBrands = _carBrandService.GetAll();
            var mappedCarBrands = _mapper.Map<IEnumerable<CarBrandResponseModel>>(carBrands);
            return Ok(mappedCarBrands);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCarBrand([FromBody] CarBrandCreateRequestModel model)
        {
            if (model == null)
                return BadRequest();

            var mappedCarBrand = _mapper.Map<CarBrandCreateRequestDTO>(model);
            _carBrandService.Create(mappedCarBrand);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("{carBrandId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCarBrand(int carBrandId, [FromBody] CarBrandUpdateRequestModel model)
        {
            if (model == null)
                return BadRequest();

            var mappedCarBrand = _mapper.Map<CarBrandUpdateRequestDTO>(model);

            _carBrandService.Update(carBrandId, mappedCarBrand);
            return NoContent();
        }

        [HttpDelete("{carBrandId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCarBrand(int carBrandId)
        {
            _carBrandService.Delete(carBrandId);
            return NoContent();
        }
    }
}