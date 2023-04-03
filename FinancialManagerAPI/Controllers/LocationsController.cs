using AutoMapper;
using FinancialManager.Controllers;
using FinancialManager.Core.DTOs;
using FinancialManager.Interfaces;
using FinancialManager.Models;
using FinancialManager.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialManagerAPI.Controllers
{
    [Route("api/locations")]
    [Authorize]
    public class LocationsController : BaseApiController
    {
        public LocationsController(IUnitOfWork unitOfWork, ILoggerManager logger, IMapper mapper)
            : base(unitOfWork, logger, mapper)
        {
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                var locations = await _unitOfWork.LocationRepository.GetAllLocations();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetAllLocations)} action {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("getById/{locationId}", Name = "GetLocationById")]
        public async Task<IActionResult> GetLocationById(int locationId)
        {
            var location = await _unitOfWork.LocationRepository.GetLocationById(locationId);
            if (location is not null)
            {
                return Ok(location);
            }
            _logger.LogInfo($"Location with id: {locationId} doesn't exist in the database.");
            return NotFound();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateLocation([FromBody] LocationCreateDto incomingLocation)
        {
            var location = _mapper.Map<Location>(incomingLocation);
            await _unitOfWork.LocationRepository.AddLocation(location);
            await _unitOfWork.SaveAsync();
            var locationReturn = _mapper.Map<LocationDto>(location);
            return CreatedAtRoute("GetLocationById", new { locationId = locationReturn.Id }, locationReturn);
        }

        [HttpDelete("delete/{locationId}")]
        public async Task<IActionResult> DeleteLocation(int locationId)
        {
            var location = await _unitOfWork.LocationRepository.GetLocationById(locationId);
            if (location is null)
            {
                return NotFound();
            }
            await _unitOfWork.LocationRepository.DeleteLocation(location);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
