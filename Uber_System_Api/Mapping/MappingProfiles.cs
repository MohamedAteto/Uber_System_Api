using AutoMapper;
using Uber_System_Api.DTOs.AuthDTOs;
using Uber_System_Api.DTOs.RideDTOs;
using Uber_System_Api.DTOs.UserDTOs;
using Uber_System_Api.DTOs.VehicleDTOs;
using Uber_System_Api.Models;

namespace Uber_System_Api.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User , LoginDTO>().ReverseMap();
            CreateMap<User , RegisterDTO>().ReverseMap();


            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();

            CreateMap<Vehicle,VehicleDTO>().ReverseMap();
            CreateMap<Vehicle,CreateVehicleDTO>().ReverseMap();
            CreateMap<Vehicle,UpdateVehicleDTO>().ReverseMap();



            CreateMap<Ride, RideDTO>().ReverseMap();
            CreateMap<Ride, CreateRideDTO>().ReverseMap();
            CreateMap<Ride, UpdateRideDTO>().ReverseMap();
           
        }
    }
}
