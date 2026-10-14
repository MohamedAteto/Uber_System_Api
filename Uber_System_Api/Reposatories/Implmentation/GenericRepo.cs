using Microsoft.EntityFrameworkCore;
using Uber_System_Api.Data;
using Uber_System_Api.Reposatories.Interface;

namespace Uber_System_Api.Reposatories.Implmentation
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var item = await _dbSet.FindAsync(id);

            _dbSet.Remove(item);

        }

        public async Task<ICollection<T>> GetAll()
        {
            var items =  await _dbSet.ToListAsync();
            return items;
        }

        public async Task<T> GetById(int id)
        {
            var item = await _dbSet.FindAsync(id);

            return item;
        }

        public async Task Update(int id, T entity)
        {
            var item = await _dbSet.FindAsync(id);

            _context.Entry(item).CurrentValues.SetValues(entity);
        }
    }
}
