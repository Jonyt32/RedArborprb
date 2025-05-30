using WebApiRedArbor.Entities;

namespace WebApiRedArbor.Interfaces
{
    public interface IServiceEmployee
    {
        Task<Employee> AddAsync(Employee entidad);
        Task DeleteAsync(int id);
        Task<Employee> GetIdAsync(int id);
        Task<IEnumerable<Employee>> ListAsync();
        Task UpdateAsync(Employee entidad);
    }
}
