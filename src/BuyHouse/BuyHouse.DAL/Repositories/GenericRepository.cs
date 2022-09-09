using BuyHouse.DAL.EF;
using BuyHouse.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BuyHouse.DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T?> GetById(int? entityId) =>
            await _dbSet.FindAsync(entityId);

        public async Task<IEnumerable<T>> GetAll() =>
            await _dbSet.ToListAsync();

        public async Task Delete(int? entityId)
        {
            var entity = await _dbSet.FindAsync(entityId);
            if (entity == null)
                throw new ArgumentException();
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChangesAsync();
        }
    }
}