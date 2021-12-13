using AutoHub.API.Models.CarModelModels;
using AutoHub.BLL.DTOs.CarModelDTOs;
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCarModels()
        {
            var carModels = _carModelService.GetAll();
            var mappedCarModels = _mapper.Map<IEnumerable<CarModelResponseModel>>(carModels);

            return Ok(mappedCarModels);
        }

        [HttpPost]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCarModel([FromBody] CarModelCreateRequestModel model)
        {
            var mappedCarModel = _mapper.Map<CarModelCreateRequestDTO>(model);
            _carModelService.Create(mappedCarModel);

            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut("{carModelId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCarModel(int carModelId, [FromBody] CarModelUpdateRequestModel model)
        {
            var mappedCarModel = _mapper.Map<CarModelUpdateRequestDTO>(model);
            _carModelService.Update(carModelId, mappedCarModel);

            return NoContent();
        }

        [HttpDelete("{carModelId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCarModel(int carModelId)
        {
            _carModelService.Delete(carModelId);

            return NoContent();
        }
    }
}