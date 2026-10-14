using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uber_System_Api.DTOs.VehicleDTOs;
using Uber_System_Api.Mapping;
using Uber_System_Api.Models;
using Uber_System_Api.Reposatories.Interface;

namespace Uber_System_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;


        public VehicleController(IUnitOfWork unit)
        {
            _unit = unit;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
        }

        [HttpPost("AddVehicle")]
        public async Task<IActionResult> AddVehicle([FromBody] CreateVehicleDTO DTO)
        {
            if (DTO == null)
                return BadRequest("Vehicle data is null.");

            var dirver = await _unit.Users.GetById(DTO.DriverId);
            if (dirver == null)
                return NotFound($"Driver with ID {DTO.DriverId} not found.");

            var Entity = _mapper.Map<Vehicle>(DTO);

            await _unit.Vehicles.Add(Entity);
            await _unit.SaveAsync();
            return Ok("Vehicle added successfully.");
        }

        [HttpPut("UpdateVehicle/{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] UpdateVehicleDTO DTO)
        {
            if (DTO == null)
                return BadRequest("Vehicle data is null.");

            var existingVehicle = await _unit.Vehicles.GetById(id);
            if (existingVehicle == null)
                return NotFound($"Vehicle with ID {id} not found.");


            var dirver = await _unit.Users.GetById(DTO.DriverId);
            if (dirver == null)
                return NotFound($"Driver with ID {DTO.DriverId} not found.");


            _mapper.Map(DTO, existingVehicle);
            await _unit.Vehicles.Update(id,existingVehicle);
            await _unit.SaveAsync();
            return Ok("Vehicle updated successfully.");

        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var existingVehicle = await _unit.Vehicles.GetById(id);
            if (existingVehicle == null)
                return NotFound($"Vehicle with ID {id} not found.");


            await _unit.Vehicles.Delete(id);
            await _unit.SaveAsync();
            return Ok("Vehicle deleted successfully.");
        }
    }
}