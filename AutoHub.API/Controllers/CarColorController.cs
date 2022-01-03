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
    [ApiController]
    [Authorize]
    [Route("api/[controller]s")]
    [Produces("application/json")]
    public class CarColorController : Controller
    {
        private readonly ICarColorService _carColorService;
        private readonly IMapper _mapper;

        public CarColorController(ICarColorService carColorService, IMapper mapper)
        {
            _carColorService = carColorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all car colors.
        /// </summary>
        /// <response code="401">Unauthorized Access.</response>
        /// <returns>Returns list of car colors.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarColorResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCarColors()
        {
            var carColors = _carColorService.GetAll();
            var mappedCarColors = _mapper.Map<IEnumerable<CarColorResponseModel>>(carColors);

            return Ok(mappedCarColors);
        }

        /// <summary>
        /// Create car color.
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CarColors
        ///     {
        ///         "carColorName": "Magenta"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Color was created successfully.</response>
        /// <response code="400">Invalid model.</response>
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
        public IActionResult CreateCarColor([FromBody] CarColorCreateRequestModel model)
        {
            var mappedCarColor = _mapper.Map<CarColorCreateRequestDTO>(model);
            _carColorService.Create(mappedCarColor);

            return StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Update car color.
        /// </summary>
        /// <param name="carColorId"></param>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /CarColors
        ///     {
        ///         "carColorName": "Magenta"
        ///     }
        /// 
        /// </remarks>
        /// <response code="204">Color was updated successfully.</response>
        /// <response code="400">Invalid model.</response>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">Color not found.</response>
        /// <returns></returns>
        [HttpPut("{carColorId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCarColor(int carColorId, [FromBody] CarColorUpdateRequestModel model)
        {
            var mappedCarColor = _mapper.Map<CarColorUpdateRequestDTO>(model);
            _carColorService.Update(carColorId, mappedCarColor);

            return NoContent();
        }

        /// <summary>
        /// Delete car color.
        /// </summary>
        /// <param name="carColorId"></param>
        /// <response code="204">Color was deleted successfully.</response>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">Color not found.</response>
        /// <returns></returns>
        [HttpDelete("{carColorId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCarColor(int carColorId)
        {
            _carColorService.Delete(carColorId);

            return NoContent();
        }
    }
}