using AutoHub.API.Models.CarColorModels;
using AutoHub.BLL.DTOs.CarColorDTOs;
using AutoHub.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using AutoHub.API.Common;
using Microsoft.AspNetCore.Authorization;

namespace AutoHub.API.Controllers
{
    [Route("api/[controller]s")]
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
        [ProducesResponseType(typeof(IEnumerable<CarColorResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCarColors()
        {
            var carColors = _carColorService.GetAll();
            var mappedCarColors = _mapper.Map<IEnumerable<CarColorResponseModel>>(carColors);

            return Ok(mappedCarColors);
        }

        [HttpPost]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCarColor([FromBody] CarColorCreateRequestModel model)
        {
            var mappedCarColor = _mapper.Map<CarColorCreateRequestDTO>(model);
            _carColorService.Create(mappedCarColor);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("{carColorId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCarColor(int carColorId, [FromBody] CarColorUpdateRequestModel model)
        {
            var mappedCarColor = _mapper.Map<CarColorUpdateRequestDTO>(model);
            _carColorService.Update(carColorId, mappedCarColor);

            return NoContent();
        }

        [HttpDelete("{carColorId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCarColor(int carColorId)
        {
            _carColorService.Delete(carColorId);

            return NoContent();
        }
    }
}