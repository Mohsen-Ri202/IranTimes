using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace IranTimes.Controllers
{
    [Authorize(Roles = "Owner")]
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> _rolemanager;
        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            _rolemanager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = _rolemanager.Roles.ToList();
            return View(roles);
        }
        public IActionResult AddRoles()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoles(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model == null) return NotFound();
                var role = new IdentityRole(model.RoleName);
                var result = await _rolemanager.CreateAsync(role);
                if (result.Succeeded) return RedirectToAction("Index");
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRole(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return NotFound();
            var role = await _rolemanager.FindByIdAsync(Id);
            if (role == null) return NotFound();
            await _rolemanager.DeleteAsync(role);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditeRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var role = await _rolemanager.FindByIdAsync(id);
            if (role == null) return NotFound();
            RoleViewModel model = new RoleViewModel()
            {
                  id=role.Id,
                  RoleName=role.Name
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditeRole(RoleViewModel model)
        {
            if (model == null) return NotFound();
            var role = await _rolemanager.FindByIdAsync(model.id);
            if (role == null) return NotFound();
            role.Name = model.RoleName;
            var result = await _rolemanager.UpdateAsync(role);
            if (result.Succeeded) return RedirectToAction("Index");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }
    }
}
