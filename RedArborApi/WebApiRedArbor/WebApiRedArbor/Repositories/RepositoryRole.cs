using System;
using WebApiRedArbor.Context;
using WebApiRedArbor.Entities;
using WebApiRedArbor.Interfaces;

namespace WebApiRedArbor.Repositories
{
    public class RepositoryRole : RepositoryGeneric<Role>
    {
        public RepositoryRole(ApplicationDbContext contexto) : base(contexto)
        {
        }


        public async Task<Role> AddAsync(Role entidad)
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

        public async Task<Role> GetIdAsync(int id)
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

        public async Task<IEnumerable<Role>> ListAsync()
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

        public async Task UpdateAsync(Role entidad)
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
