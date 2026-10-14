namespace Uber_System_Api.Reposatories.Interface
{
    public interface IGenericRepo<T> where T : class
    {
        Task<ICollection<T>> GetAll();
        Task<T> GetById(int id);

        Task  Add(T entity);
        Task Update(int id,T entity);
        Task Delete(int id);

    }
}
