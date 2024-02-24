using Employee_Management_System.Data;
using Employee_Management_System.Models;

namespace Employee_Management_System.Interface
{
    public interface IDepartmentRepository
    {

        public  Task<IEnumerable<Department>> GetAll();


        public  Task<Department> GetByIdAsync(int id);

        public Task<Department> GetByIdAsyncNoTracking(int id);

        bool Update(Department department);

        bool Delete(Department department);

        bool Add(Department department);

        bool Save();
    }
}
