using WebApiRedArbor.Entities;
using WebApiRedArbor.Interfaces;

namespace WebApiRedArbor.Services
{
    public class ServiceRole: IServiceRole
    {
        private readonly IRepositoryGeneric<Role> _repository;
        public ServiceRole(IRepositoryGeneric<Role> repository) 
        {
            _repository = repository;
        }

        public async Task<Role> AddAsync(Role entidad)
        {
            try
            {
                return await _repository.AddAsync(entidad);

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
                await _repository.DeleteAsync(id);
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
                return await _repository.GetIdAsync(id);
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
                return await _repository.ListAsync();
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
                await _repository.UpdateAsync(entidad);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
