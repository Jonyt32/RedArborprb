using WebApiRedArbor.Entities;
using WebApiRedArbor.Interfaces;

namespace WebApiRedArbor.Services
{
    public class ServiceEmployee: IServiceEmployee
    {
        private readonly IRepositoryEmployee _repository;
        private readonly IRepositoryGeneric<Role> _roleRepository;
        public ServiceEmployee(IRepositoryEmployee repository, IRepositoryGeneric<Role> roleRepository) 
        {
            _repository = repository;
            _roleRepository = roleRepository;
        }
        public async Task<Employee> AddAsync(Employee entidad)
        {
            try
            {
                var employeeExist = await _repository.GetEmployeeEmailAsync(entidad.Email);
                if (employeeExist == null)
                {
                    await ValidateRole(entidad.RoleId);
                    return await _repository.AddAsync(entidad);
                }
                else 
                {
                    throw new Exception("El empleado ya existe");
                }
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

        public async Task<Employee> GetIdAsync(int id)
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

        public async Task<IEnumerable<Employee>> ListAsync()
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

        public async Task UpdateAsync(Employee entidad)
        {
            try
            {
                await ValidateRole(entidad.RoleId);
                await _repository.UpdateAsync(entidad);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private async Task ValidateRole(int idRole) 
        {
            try
            {
                var roleExist = await _roleRepository.GetIdAsync(idRole);
                if (roleExist == null) 
                {
                    throw new Exception("Debe ingresar un role valido");
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
