using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var locations = await _locationService.GetAllLocations();
            if (locations == null)
            {
                return NotFound();
            }
            return Ok(locations);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var location = await _locationService.GetLocation(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        [HttpGet("{locationName}")]
        public async Task<IActionResult> Get(string locationName)
        {
            var location = await _locationService.GetLocation(locationName);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LocationRequestDTO locationRequestDTO)
        {
            if (locationRequestDTO == null)
            {
                return BadRequest("Invalid data provided.");
            }
            var location = await _locationService.CreateLocation(locationRequestDTO);
            return Ok(location);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LocationRequestDTO locationRequestDTO)
        {
            if (id <= 0 || locationRequestDTO == null)
            {
                return BadRequest("Invalid data provided.");
            }
            var location = await _locationService.GetLocation(id);
            if (location == null)
            {
                return NotFound();
            }
            var updatedLocation = await _locationService.UpdateLocation(id, locationRequestDTO);
            return Ok(updatedLocation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid data provided.");
            }
            var location = await _locationService.GetLocation(id);
            if (location == null)
            {
                return NotFound();
            }
            var deletedLocationId = await _locationService.DeleteLocation(id);
            return Ok(deletedLocationId);
        }
    } 
}
