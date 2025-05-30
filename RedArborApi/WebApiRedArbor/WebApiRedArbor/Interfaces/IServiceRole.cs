using WebApiRedArbor.Entities;

namespace WebApiRedArbor.Interfaces
{
    public interface IServiceRole
    {
        Task<Role> AddAsync(Role entidad);
        Task DeleteAsync(int id);
        Task<Role> GetIdAsync(int id);
        Task<IEnumerable<Role>> ListAsync();
        Task UpdateAsync(Role entidad);
    }
}
