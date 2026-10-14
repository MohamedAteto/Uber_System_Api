using Uber_System_Api.Models;

namespace Uber_System_Api.Reposatories.Interface
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User> GetUserByUserNameAsync(string Name);
        Task<ICollection<User>> GetDrivers();

        Task<ICollection<User>> GetRidersByName(string Name);
    }
}
