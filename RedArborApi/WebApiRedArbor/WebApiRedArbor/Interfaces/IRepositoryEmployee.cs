using WebApiRedArbor.Entities;

namespace WebApiRedArbor.Interfaces
{
    public interface IRepositoryEmployee: IRepositoryGeneric<Employee>
    {
        Task<Employee> GetEmployeeEmailAsync(string email);
    }
}
