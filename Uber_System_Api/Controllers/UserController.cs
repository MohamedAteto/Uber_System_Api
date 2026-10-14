using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uber_System_Api.Mapping;
using Uber_System_Api.Reposatories.Interface;

namespace Uber_System_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;


        public UserController(IUnitOfWork unit)
        {
            _unit = unit;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
        }



        [HttpGet("GetAllDrivers")]
        public async Task<IActionResult> GetAllDrivers()
        {
            var drivers = await _unit.Users.GetDrivers();

            if(drivers == null || !drivers.Any())
                return NotFound("No drivers found.");


            
            return Ok(drivers);
        }
    }
}
