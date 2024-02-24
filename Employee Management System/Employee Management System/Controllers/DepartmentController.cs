using Employee_Management_System.Interface;
using Employee_Management_System.Models;
using Employee_Management_System.Repository;
using Employee_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DepartmentController(IDepartmentRepository departmentRepository,IEmployeeRepository employeeRepository)
        {
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Department> departments = await _departmentRepository.GetAll();
            return View(departments);
        }

        [Authorize(Roles = "Update")]
        public async Task<IActionResult> Edit(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null) { return View("Error"); }
            var departmentVM = new EditDepartmentViewModel()
            {
                Id = id,
                DepartmentName = department.DepartmentName,
            };
            return View(departmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id ,EditDepartmentViewModel editDepartmentViewModel)
        {
            if(!ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Değiştirirken bir hata oluştu");
                return View("Error",editDepartmentViewModel);
            }

            var userDepartment = _departmentRepository.GetByIdAsyncNoTracking(id);
            if (userDepartment == null) { return View("Error"); }

            var department = new Department
            {
                Id = id,
                DepartmentName = editDepartmentViewModel.DepartmentName,
            };

            _departmentRepository.Update(department);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Delete")]
        public async  Task<IActionResult> Delete(int id) 
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if(department == null) { return View("Error"); }
            return View(department);
        }

        [HttpPost , ActionName("Delete")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            
            var userDepartment = await _departmentRepository.GetByIdAsync(id);

            
            if (userDepartment == null) 
            {
                return View("Error");
            }

            var employeesInDepartment = await _employeeRepository.GetEmployeeByDepartment(userDepartment.DepartmentName);

            if (employeesInDepartment.Any())
            {
                TempData["Error"] = "Bu departmanda çalışanlar var, departmanı silemezsiniz.";
                return View(userDepartment); // veya istediğiniz bir sayfaya yönlendirme yapabilirsiniz
            }

             

            _departmentRepository.Delete(userDepartment);
            return RedirectToAction("Index");
            

        }

        [Authorize(Roles = "Create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if(!ModelState.IsValid)
            {
                return View("Error");
            }

            _departmentRepository.Add(department);
            return RedirectToAction("Index");
        }

        [NonAction]
        public JsonResult Detail(int id) 
        {
            var departments = _departmentRepository.GetByIdAsync(id);
            return Json(departments);
        }
    } 
}
