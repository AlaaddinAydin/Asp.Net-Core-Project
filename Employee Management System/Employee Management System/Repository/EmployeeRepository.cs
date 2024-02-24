using Employee_Management_System.Data;
using Employee_Management_System.Interface;
using Employee_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public EmployeeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool Add(Employee employee)
        {
            _applicationDbContext.Add(employee);
            return Save();
        }

        public bool Delete(Employee employee)
        {
            _applicationDbContext.Remove(employee);
            return Save();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _applicationDbContext.Employees.Include(a => a.Department).ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Employees.Include(a => a.Department).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Employee> GetByIdAsyncNoTracking(int id)
        {
            return await _applicationDbContext.Employees.AsNoTracking().Include(a => a.Department).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByDepartment(string department)
        {
            return await _applicationDbContext.Employees.Where(c => c.Department.DepartmentName.Contains(department)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _applicationDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Employee employee)
        {
            
            _applicationDbContext.Update(employee);
            return Save();
        }
    }
}
