using Uber_System_Api.Models;

namespace Uber_System_Api.Reposatories.Interface
{
    public interface IUnitOfWork
    {
        IUserRepo Users { get; }
        IRideRepo Rides { get; }
        IGenericRepo<Vehicle> Vehicles { get; }
        //IPaymentRepo Payments { get; }
        //IRatingRepo Ratings { get; }
        Task SaveAsync();
    }
}
