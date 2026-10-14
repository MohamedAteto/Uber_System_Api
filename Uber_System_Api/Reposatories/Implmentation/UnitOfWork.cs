using Uber_System_Api.Data;
using Uber_System_Api.Models;
using Uber_System_Api.Reposatories.Interface;

namespace Uber_System_Api.Reposatories.Implmentation
{
    public class UnitOfWork : IUnitOfWork
    {
        public IUserRepo Users { get; }
        public IGenericRepo<Vehicle> Vehicles { get; }

        public IRideRepo Rides { get; }

        private AppDbContext _context;

        public UnitOfWork(AppDbContext context, IUserRepo users , IGenericRepo<Vehicle> vehicle , IRideRepo rides)
        {
            _context = context;
            Users = users;
            Vehicles = vehicle;
            Rides = rides;
        }



        public async Task SaveAsync()
        {
             _context.SaveChanges();
        }
    }
}
