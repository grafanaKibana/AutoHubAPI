using System;
using System.Collections.Generic;
using AutoHub.BLL.Interfaces;
using AutoHub.BLL.Models.CarColorModels;
using AutoHub.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarColorController : Controller
    {
        private readonly ICarColorService _carColorService;
        private readonly IMapper _mapper;

        public CarColorController(ICarColorService carColorService, IMapper mapper)
        {
            _carColorService = carColorService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var carColors = _carColorService.GetAll();
                var mappedCarColors = _mapper.Map<IEnumerable<CarColorResponseModel>>(carColors);
                return Ok(mappedCarColors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public IActionResult CreateCarColor([FromBody] CarColorCreateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();

                var mappedCarColor = _mapper.Map<CarColor>(model);
                _carColorService.CreateCarColor(mappedCarColor);

                return StatusCode(201, model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCarColor(int id, [FromBody] CarColorUpdateRequestModel model)
        {
            try
            {
                if (model == null)
                    return BadRequest();
                if (_carColorService.GetById(id) == null)
                    return NotFound();

                var mappedCarColor = _mapper.Map<CarColor>(model);
                _carColorService.UpdateCarColor(mappedCarColor);
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}