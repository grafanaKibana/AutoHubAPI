using System;
using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarModelModels;
using AutoHub.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]")]
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

                var mappedCarModel = _mapper.Map<CarModel>(model);
                _carModelService.CreateCarModel(mappedCarModel);
                return StatusCode(201, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCarModel(int id, [FromBody] CarModelUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                if (_carModelService.GetById(id) == null)
                    return NotFound();

                var mappedCarModel = _mapper.Map<CarModel>(model);
                _carModelService.UpdateCarModel(mappedCarModel);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}