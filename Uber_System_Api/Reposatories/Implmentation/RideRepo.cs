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
