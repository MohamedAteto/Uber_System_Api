using Microsoft.EntityFrameworkCore;
using Uber_System_Api.Data;
using Uber_System_Api.Models;
using Uber_System_Api.Reposatories.Interface;

namespace Uber_System_Api.Reposatories.Implmentation
{
    public class RideRepo : GenericRepo<Ride>, IRideRepo
    {
        private readonly AppDbContext _context;
        public RideRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AcceptRide(int rideid)
        {
            var item = await _context.Rides.FindAsync(rideid);
          
                item.Status = "Accepted";
                await _context.SaveChangesAsync();
            
        }

        public async Task<ICollection<Ride>> GetRidesByPickUpLocation(string Location)
        {
            var items = await _context.Rides.Where(l => l.PickupLocation.Contains(Location)).ToListAsync();
            return items;
        }

        public async Task<ICollection<Ride>> GetRidesWithFilteration(int? MinFare, int? MaxFare, string? Status)
        {
            var query = _context.Rides.AsQueryable();

            if (MinFare.HasValue)
            {
                query = query.Where(r => r.Fare >= MinFare.Value);
            }

            if (MaxFare.HasValue)
            {
                query = query.Where(r => r.Fare <= MaxFare.Value);
            }

            if (!string.IsNullOrEmpty(Status))
            {
                query = query.Where(r => r.Status == Status);
            }

            query = query.OrderByDescending(r => r.rating);

            return await query.ToListAsync();
        }


        public async Task<ICollection<Ride>> GetRidesWithPagignation(int pageNumber, int pageSize)
        {
            var items = await _context.Rides
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return items;
        }

        public async Task MarkComplete(int rideid)
        {
            var item = await _context.Rides.FindAsync(rideid);

            item.Status = "Completed";
            await _context.SaveChangesAsync();
        }
    }
}
