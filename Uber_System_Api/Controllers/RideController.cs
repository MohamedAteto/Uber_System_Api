using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Uber_System_Api.DTOs.RideDTOs;
using Uber_System_Api.DTOs.VehicleDTOs;
using Uber_System_Api.Mapping;
using Uber_System_Api.Models;
using Uber_System_Api.Reposatories.Interface;

namespace Uber_System_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RideController : ControllerBase
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;


        public RideController(IUnitOfWork unit)
        {
            _unit = unit;
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfiles>()).CreateMapper();
        }



        [HttpGet("GetAllRides")]
        public async Task<IActionResult> GetAllRides(int pagenumber,int pagesize)
        {
            var rides = await _unit.Rides.GetRidesWithPagignation(pagenumber, pagesize);

            if(rides == null || !rides.Any())
                return NotFound("No rides found.");

            
            var DTO = _mapper.Map<IEnumerable<RideDTO>>(rides);
            return Ok(DTO);
        }


        [HttpPost("AddRide")]
        public async Task<IActionResult> AddVehicle([FromBody] CreateRideDTO DTO)
        {
            if (DTO == null)
                return BadRequest("Ride data is null.");

            var dirver = await _unit.Rides.GetById(DTO.DriverId);
            if (dirver == null)
                return NotFound($"Driver with ID {DTO.DriverId} not found.");

            var Rider = await _unit.Rides.GetById(DTO.RiderId);
            if (Rider == null)
                return NotFound($"Rider with ID {DTO.RiderId} not found.");



            var Entity = _mapper.Map<Ride>(DTO);

            await _unit.Rides.Add(Entity);
            await _unit.SaveAsync();
            return Ok("Vehicle added successfully.");
        }


        [HttpGet("GetRideById/{id}")]
        public async Task<IActionResult> GetRideById(int id)
        {
            var ride = await _unit.Rides.GetById(id);
            if (ride == null)
                return NotFound($"Ride with ID {id} not found.");


            var DTO = _mapper.Map<RideDTO>(ride);
            return Ok(DTO);
        }

        [HttpPatch("Accept_Ride")]
        public async Task<IActionResult> AcceptTask( int rideid)
        {
            var item = await _unit.Rides.GetById(rideid);
            if (item == null)
                return NotFound($"Ride with ID {rideid} not found.");

            var deriver = await _unit.Users.GetById(item.DriverId);
            if (deriver == null)
                return NotFound($"Driver with ID {item.DriverId} not found.");

            var rider = await _unit.Users.GetById(item.RiderId);
            if (rider == null)
                return NotFound($"Rider with ID {item.RiderId} not found.");

            await _unit.Rides.AcceptRide(rideid);
            await _unit.SaveAsync();

            return Ok("Ride accepted successfully.");

        }

        [HttpPatch("Complete_Ride/")]
        public async Task<IActionResult> CompleteRide( int rideid)
        {
            var item = await _unit.Rides.GetById(rideid);
            if (item == null)
                return NotFound($"Ride with ID {rideid} not found.");

            var deriver = await _unit.Users.GetById(item.DriverId);
            if (deriver == null)
                return NotFound($"Driver with ID {item.DriverId} not found.");

            var rider = await _unit.Users.GetById(item.RiderId);
            if (rider == null)
                return NotFound($"Rider with ID {item.RiderId} not found.");

            await _unit.Rides.MarkComplete(rideid);
            await _unit.SaveAsync();
            return Ok("Ride completed successfully.");
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var existingVehicle = await _unit.Rides.GetById(id);
            if (existingVehicle == null)
                return NotFound($"Ride with ID {id} not found.");


            await _unit.Rides.Delete(id);
            await _unit.SaveAsync();
            return Ok("Ride deleted successfully.");
        }

        [HttpGet("GetRides With the same Pip/Location")]
        public async Task<IActionResult> GetRidesWithPip(string Location)
        {
            if (Location is null)
                return BadRequest();

            var items = await _unit.Rides.GetRidesByPickUpLocation(Location);

            if (items is null)
                return BadRequest("There is not rides for this ");

            var DTO = _mapper.Map<List<RideDTO>>(items);

            return Ok(DTO);
        }

        [HttpGet("Git with filters")]
        public async Task<IActionResult> Filteration(int? MaxFare,int? MinFare , string? Status)
        {
            var items = await _unit.Rides.GetRidesWithFilteration(MinFare, MaxFare, Status);

            if (items is null)
                return BadRequest("There isn't Rides with this Filteration !!!");

            var DTO = _mapper.Map<List<RideDTO>>(items);

            return Ok(DTO);
        }
    }
}
