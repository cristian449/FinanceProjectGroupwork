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
        public async Task<IActionResult> UserManaging()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }


        [HttpGet]
        public async Task<IActionResult> UserDetails(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(i => i.Id == id);
            if (user == null)
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



        //Currently seems that when making a user into an Admin, the new Admin does not need the AdminKey
        // to access Admin features. This may need to be changed in the future.

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
