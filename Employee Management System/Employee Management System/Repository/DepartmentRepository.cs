using Employee_Management_System.Data;
using Employee_Management_System.Interface;
using Employee_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;

namespace Employee_Management_System.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DepartmentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool Add(Department department)
        {
            _applicationDbContext.Add(department);
            return Save();
        }

        public bool Delete(Department department)
        {
            _applicationDbContext.Remove(department);
            return Save();
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _applicationDbContext.Departments.ToListAsync();
        }

        

        public async Task<Department> GetByIdAsync(int id)
        {
            
            return await _applicationDbContext.Departments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Department> GetByIdAsyncNoTracking(int id)
        {

            return await _applicationDbContext.Departments.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _applicationDbContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Department department)
        {
            _applicationDbContext.Update(department);
            return Save();
        }
    }
}
