using Employee_Management_System.Models;
using Employee_Management_System.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Employee_Management_System.Controllers
{
    [Authorize(Roles ="admin")]
    public class UserRoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRoleController(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var values = _userManager.Users.ToList();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> Assign(string id) 
        {
            var user = _userManager.Users.FirstOrDefault(i => i.Id == id);
            var roles = _roleManager.Roles.ToList();

            TempData["UserId"] = user.Id;

            var userRoles = await _userManager.GetRolesAsync(user);

            List<AssingRoleViewModel> AssingRoleViewModel = new List<AssingRoleViewModel>();

            foreach (var item in roles)
            {
                AssingRoleViewModel m = new AssingRoleViewModel();
                m.RoleId = item.Id;
                m.Name = item.Name;
                m.Exists = userRoles.Contains(item.Name);
                AssingRoleViewModel.Add(m);
            }

            return View(AssingRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(List<AssingRoleViewModel> assingRoleVM)
        {
            if (TempData["UserId"] is Guid userId)
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id == userId.ToString());

                if (user != null)
                {
                    // Seçilen rollerin kullanıcıya atanması
                    foreach (var item in assingRoleVM)
                    {
                        if (item.Exists)
                        {
                            await _userManager.AddToRoleAsync(user, item.Name);
                        }
                        else
                        {
                            await _userManager.RemoveFromRoleAsync(user, item.Name);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }


    }
}
