using System.Globalization;
using FinanceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> UserManaging(SortUsersBy? sortBy)
        {
            var users = _userManager.Users.ToList();

            users = sortBy switch
            {
                SortUsersBy.FirstName => users.OrderBy(u => u.FirstName).ToList(),
                SortUsersBy.CreatedAt => users.OrderBy(u => u.CreatedAt).ToList(),
                _ => users
            };
            ViewBag.SortBy = sortBy;
            if (!User.IsInRole("Admin"))
            {
                return NotFound();
            }
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> UserDetails(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(i => i.Id == id);
            if (user == null || !User.IsInRole("Admin"))
            {
                return NotFound();
            }
            return View(user);

        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("UserManaging");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRole(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return RedirectToAction("UserManaging");
        }
    }
}
