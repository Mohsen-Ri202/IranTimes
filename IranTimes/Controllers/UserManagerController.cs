using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IranTimes.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace IranTimes.Controllers
{
    //[Authorize(Roles = "Owner")]
    public class UserManagerController : Controller
    {
        private RoleManager<IdentityRole> _roleManager;
        private UserManager<ApplicationUser> _userManager;

        public UserManagerController
            (RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var model = _userManager.Users.Select(s => new IndexViewModel()
            {
                Id = s.Id,
                UserName = s.UserName,
                Email = s.Email,

            }).ToList();

            foreach (var item in model)
            {
                var user = await _userManager.FindByEmailAsync(item.Email);
                var role = await _userManager.GetRolesAsync(user);

                item.RoleName = role;
            }


            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditeUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            IdentityUserViewModel model = new IdentityUserViewModel()
            {
                Id = user.Id,
                Name = user.UserName
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditeUser(IdentityUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();
            user.UserName = model.Name;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return RedirectToAction("Index");

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var rols = _roleManager.Roles.ToList();
            var userrole = await _userManager.GetRolesAsync(user);
            var valid = rols.Where(w => !userrole.Contains(w.Name)).ToList();

            var model = new AddUserToRoleViewModel()
            {
                Id = id
            };
            foreach (var role in valid)
            {
                model.UserRoleNames.Add(new UserRoleName()
                {
                    RoleName = role.Name
                });
            }


            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model)
        {

            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();
            var rolerequest = model.UserRoleNames
                .Where(w => w.IsSelected)
                .Select(s => s.RoleName)
                .ToList();

            var result = await _userManager.AddToRolesAsync(user, rolerequest);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var userrole = await _userManager.GetRolesAsync(user);
            var model = new AddUserToRoleViewModel()
            {
                Id = id
            };
            foreach (var role in userrole)
            {
                model.UserRoleNames.Add(new UserRoleName()
                {
                    RoleName = role
                });
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserFromRole(AddUserToRoleViewModel model)
        {
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();
            var roles = _roleManager.Roles.ToList();

            var requestrole = model.UserRoleNames
                .Where(w => w.IsSelected)
                .Select(s => s.RoleName)
                .ToList();
            var result = await _userManager.RemoveFromRolesAsync(user, requestrole);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }


    }
}
