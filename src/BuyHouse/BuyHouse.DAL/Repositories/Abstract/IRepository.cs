namespace BuyHouse.DAL.Repositories.Abstract
{
    public interface IRepository<T> 
    { 
        public Task Create(T entity);
        public void Update(T entity);
        public Task Delete(int? entity);
        public Task<T?> GetById(int? entity);
        public Task<IEnumerable<T>> GetAll();
    }
}