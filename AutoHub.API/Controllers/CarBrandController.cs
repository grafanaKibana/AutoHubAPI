using AutoHub.API.Models.CarBrandModels;
using AutoHub.BLL.DTOs.CarBrandDTOs;
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
    public class CarBrandController : Controller
    {
        private readonly ICarBrandService _carBrandService;
        private readonly IMapper _mapper;

        public CarBrandController(ICarBrandService carBrandService, IMapper mapper)
        {
            _carBrandService = carBrandService;
            _mapper = mapper;
        }


        /// <summary>
        /// Get all car brands.
        /// </summary>
        /// <response code="401">Unauthorized Access.</response>
        /// <returns>Returns list of car brands</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CarBrandResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllCarBrands()
        {
            var carBrands = _carBrandService.GetAll();
            var mappedCarBrands = _mapper.Map<IEnumerable<CarBrandResponseModel>>(carBrands);

            return Ok(mappedCarBrands);
        }

        /// <summary>
        /// Create car brand.
        /// </summary>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /CarBrands
        ///     {
        ///         "carBrandName": "Audi"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Brand was created successfully</response>
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
        public IActionResult CreateCarBrand([FromBody] CarBrandCreateRequestModel model)
        {
            var mappedCarBrand = _mapper.Map<CarBrandCreateRequestDTO>(model);
            _carBrandService.Create(mappedCarBrand);

            return StatusCode((int)HttpStatusCode.Created);
        }

        /// <summary>
        /// Update car brand.
        /// </summary>
        /// <param name="carBrandId"></param>
        /// <param name="model"></param>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /CarBrands
        ///     {
        ///         "carBrandName": "Audi"
        ///     }
        /// 
        /// </remarks>
        /// <response code="204">Brand was updated successfully</response>
        /// <response code="400">Invalid model</response>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">Brand not found</response>
        /// <returns></returns>
        [HttpPut("{carBrandId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCarBrand(int carBrandId, [FromBody] CarBrandUpdateRequestModel model)
        {
            var mappedCarBrand = _mapper.Map<CarBrandUpdateRequestDTO>(model);
            _carBrandService.Update(carBrandId, mappedCarBrand);

            return NoContent();
        }


        /// <summary>
        /// Delete car brand.
        /// </summary>
        /// <param name="carBrandId"></param>
        /// <response code="204">Brand was deleted successfully</response>
        /// <response code="401">Unauthorized Access.</response>
        /// <response code="403">Admin access only.</response>
        /// <response code="404">Brand not found</response>
        /// <returns></returns>
        [HttpDelete("{carBrandId}")]
        [Authorize(Roles = AuthorizationRoles.Administrator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCarBrand(int carBrandId)
        {
            _carBrandService.Delete(carBrandId);

            return NoContent();
        }
    }
}