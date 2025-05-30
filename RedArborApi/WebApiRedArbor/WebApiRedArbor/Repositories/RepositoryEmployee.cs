using Microsoft.EntityFrameworkCore;
using WebApiRedArbor.Context;
using WebApiRedArbor.Entities;
using WebApiRedArbor.Interfaces;

namespace WebApiRedArbor.Repositories
{
    public class RepositoryEmployee: RepositoryGeneric<Employee>, IRepositoryEmployee
    {
        public RepositoryEmployee(ApplicationDbContext contexto) : base(contexto)
        {
        }
        public async Task<Employee> AddAsync(Employee entidad)
        {
            try
            {
                return await base.AddAsync(entidad);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await base.DeleteAsync(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> GetIdAsync(int id)
        {
            try
            {
                return await base.GetIdAsync(id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> GetEmployeeEmailAsync(string email) 
        {
            try
            {
                return await _dbSet.Where(x => x.Email == email).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<Employee>> ListAsync()
        {
            try
            {
                return await base.ListAsync();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateAsync(Employee entidad)
        {
            try
            {
                await base.UpdateAsync(entidad);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
