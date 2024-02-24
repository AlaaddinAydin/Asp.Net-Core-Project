using Employee_Management_System.Models;

namespace Employee_Management_System.Interface
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAll();

        Task<Employee> GetByIdAsync(int id);
        Task<Employee> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Employee>> GetEmployeeByDepartment(string department);

        bool Add(Employee employee);

        bool Update(Employee employee);

        bool Delete(Employee employee);

        bool Save();
    }
}
