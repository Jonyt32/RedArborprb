namespace WebApiRedArbor.Interfaces
{
    public interface IRepositoryGeneric<T> where T : class
    {
        Task<IEnumerable<T>> ListAsync();
        Task<T> GetIdAsync(int id);
        Task<T> AddAsync(T entidad);
        Task UpdateAsync(T entidad);
        Task DeleteAsync(int id);
    }
}
