using Employee_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Employee_Management_System.Controllers
{
    [Authorize(Roles = "admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel createRoleViewModel)
        {
            if(ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                Name = createRoleViewModel.name,
                };
                var result = await _roleManager.CreateAsync(role);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            
            return View("Error");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role = _roleManager.Roles.FirstOrDefault(i => i.Id == id);
            if(role == null)
            {
                return View("Error");
            }

            return View(role);
            
        }

        [HttpPost , ActionName("Delete")]
        public IActionResult DeleteRole(string id) 
        {
            var roleDetails = _roleManager.Roles.FirstOrDefault(r => r.Id == id);
            if (roleDetails == null) return View("Error");

            _roleManager.DeleteAsync(roleDetails);
            return RedirectToAction("Index");
        }


        public ActionResult Edit(string id) 
        {
            var roleDetails = _roleManager.Roles.FirstOrDefault(r => r.Id == id);
            if( roleDetails == null) return View("Error");

            var editRoleViewModel = new EditRoleViewModel()
            {
                Id = id,
                name = roleDetails.Name,
            };

            return View(editRoleViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id , EditRoleViewModel editRoleViewModel)
        {

            if(!ModelState.IsValid) 
            {
                ModelState.AddModelError("", "Bir şeyler eksik");
                return View("Error",editRoleViewModel);
            }

            var roleDetails = _roleManager.Roles.Where(r => r.Id == editRoleViewModel.Id).FirstOrDefault();
            if (roleDetails == null) return View("Error");

           roleDetails.Name = editRoleViewModel.name;

            var result = await _roleManager.UpdateAsync(roleDetails);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(editRoleViewModel);
            
        }
    }
}
