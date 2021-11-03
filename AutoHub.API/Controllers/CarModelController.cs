using System;
using System.Collections.Generic;
using System.Net;
using AutoHub.API.Models.CarModelModels;
using AutoHub.BLL.DTOs.CarModelDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CarModelController : Controller
    {
        private readonly ICarModelService _carModelService;
        private readonly IMapper _mapper;

        public CarModelController(ICarModelService carModelService, IMapper mapper)
        {
            _carModelService = carModelService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarModelResponseModel>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            try
            {
                var carModels = _carModelService.GetAll();
                var mappedCarModels = _mapper.Map<IEnumerable<CarModelResponseModel>>(carModels);
                return Ok(mappedCarModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateCarModel([FromBody] CarModelCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedCarModel = _mapper.Map<CarModelCreateRequestDTO>(model);
                _carModelService.CreateCarModel(mappedCarModel);
                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{carModelId}")]
        public IActionResult UpdateCarModel(int carModelId, [FromBody] CarModelUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                if (_carModelService.GetById(carModelId) == null)
                    return NotFound();

                var mappedCarModel = _mapper.Map<CarModelUpdateRequestDTO>(model);

                _carModelService.UpdateCarModel(carModelId, mappedCarModel);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}