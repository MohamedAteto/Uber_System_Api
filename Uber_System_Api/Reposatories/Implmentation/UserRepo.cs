using Microsoft.EntityFrameworkCore;
using Uber_System_Api.Data;
using Uber_System_Api.Models;
using Uber_System_Api.Reposatories.Interface;

namespace Uber_System_Api.Reposatories.Implmentation
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {

        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetDrivers()
        {
            var items = await _context.Users.Where(u => u.Role == "Driver").ToListAsync();
            return items;
        }

        public async Task<ICollection<User>> GetRidersByName(string Name)
        {
            var items = await _context.Users.Where(u => u.Role == "Rider" && u.UserFullName.Contains(Name))
                .ToListAsync();
            return items;
        }

        public async Task<User> GetUserByUserNameAsync(string Name)
        {
            var item = await _context.Users.FirstOrDefaultAsync(u => u.UserFullName == Name);
            if (item is null)
                return null;

            return item;
        }
    }
}
