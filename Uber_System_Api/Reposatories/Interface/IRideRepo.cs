using Uber_System_Api.Models;

namespace Uber_System_Api.Reposatories.Interface
{
    public interface IRideRepo : IGenericRepo<Ride>
    {
        Task<ICollection<Ride>> GetRidesWithPagignation(int pageNumber, int pageSize);
        Task AcceptRide(int rideid);
        Task MarkComplete(int rideid);
        Task<ICollection<Ride>> GetRidesByPickUpLocation(string Location);
        Task<ICollection<Ride>> GetRidesWithFilteration(int? MinFare, int? MaxFare, string? Status);
    }
}
