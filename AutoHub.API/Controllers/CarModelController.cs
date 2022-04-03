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
using System;

namespace AutoHub.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]s")]
    [Produces("application/json")]
    public class CarModelController : Controller
    {
        private readonly ICarModelService _carModelService;
        private readonly IMapper _mapper;

        public CarModelController(ICarModelService carModelService, IMapper mapper)
        {
            _carModelService = carModelService ?? throw new ArgumentNullException(nameof(carModelService));
            _mapper = mapper;
        }

        /// <summary>
        /// Get all car models.
        /// </summary>
        /// <response code="401">Unauthorized Access.</response>
        /// <returns>Returns list of car models</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarModelResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCarModels()
        {
            var carModels = _carModelService.GetAll();
            var mappedCarModels = _mapper.Map<IEnumerable<CarModelResponseModel>>(carModels);

            return Ok(mappedCarModels);
        }

        /// <summary>
        /// Create car model.
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CarModels
        ///     {
        ///         "carModelName": "RS6 Avant"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Model was created successfully</response>
        /// <response code="400">Invalid model</response>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCarModel([FromBody] CarModelCreateRequestModel model)
        {
            var mappedCarModel = _mapper.Map<CarModelCreateRequestDTO>(model);
            _carModelService.Create(mappedCarModel);

            return StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Updates car model
        /// </summary>
        /// <param name="carModelId"></param>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /CarModels
        ///     {
        ///         "carModelName": "RS6 Avant"
        ///     }
        /// 
        /// </remarks>
        /// <response code="204">Model was updated successfully</response>
        /// <response code="400">Invalid model</response>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">Model not found</response>
        /// <returns></returns>
        [HttpPut("{carModelId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCarModel(int carModelId, [FromBody] CarModelUpdateRequestModel model)
        {
            var mappedCarModel = _mapper.Map<CarModelUpdateRequestDTO>(model);
            _carModelService.Update(carModelId, mappedCarModel);

            return NoContent();
        }

        /// <summary>
        /// Deletes car model
        /// </summary>
        /// <param name="carModelId"></param>
        /// <response code="204">Model was deleted successfully</response>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">Model not found</response>
        /// <returns></returns>
        [HttpDelete("{carModelId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCarModel(int carModelId)
        {
            _carModelService.Delete(carModelId);

            return NoContent();
        }
    }
}