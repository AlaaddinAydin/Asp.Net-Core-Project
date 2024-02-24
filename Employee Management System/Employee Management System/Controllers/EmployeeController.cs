using Employee_Management_System.Interface;
using Employee_Management_System.Models;
using Employee_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Employee_Management_System.Controllers
{
    [Authorize(Roles ="admin , user")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly UserManager<AppUser> _userManager;

        public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository , UserManager<AppUser> userManager ) 
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable <Employee> employees = await _employeeRepository.GetAll();
            return View(employees);
        }

        
        public async Task<IActionResult> Detail(int id) 
        {
            Employee employee = await _employeeRepository.GetByIdAsync(id);
            return View(employee);
        }

        [Authorize(Roles = "Update")]
        public async Task<IActionResult> Edit(int id) 
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) { return View("Error"); }
            var employeeVM = new EditEmployeeViewModel()
            {
                Department = employee.Department,
                departmentId = employee.departmentId,
                Image = employee.Image,
                Name = employee.Name,
                Salary = employee.Salary,
                Surname = employee.Surname,
            };

            ViewBag.Departments = await _departmentRepository.GetAll();

            return View(employeeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditEmployeeViewModel editEmployeeVM)
        {
            if(!ModelState.IsValid) 
            {
                ModelState.AddModelError("","Bir şeyleri eksik yaptınız");
                return View("Error",editEmployeeVM);
            }

            var userEmployee = await _employeeRepository.GetByIdAsyncNoTracking(id);
            if (userEmployee == null)
            {
                return View("Error");
            }

            var selecteddep = await _departmentRepository.GetByIdAsync(editEmployeeVM.SelectedDepId);

            var employee = new Employee
            {
                Id = id,
                Name = editEmployeeVM.Name,
                Salary = editEmployeeVM.Salary,
                Department = selecteddep,
                departmentId = editEmployeeVM.SelectedDepId,
                Image = editEmployeeVM.Image,
                Surname = editEmployeeVM.Surname,
            };


            _employeeRepository.Update(employee);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var employeeDetails = await _employeeRepository.GetByIdAsync(id);
            if (employeeDetails == null)
            {
                return View("Error");
            }

            ViewBag.Departments = await _departmentRepository.GetAll();

            return View(employeeDetails);
        }

        [HttpPost , ActionName("Delete")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employeeDetails = await _employeeRepository.GetByIdAsync(id);
            if(employeeDetails == null) return View("Error");

            _employeeRepository.Delete(employeeDetails);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Create")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return View(employee);
            }

            _employeeRepository.Add(employee);
            return RedirectToAction("Index");
        }

        
    }
}
