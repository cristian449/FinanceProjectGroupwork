using FinanceProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceProject.Controllers
{
    public class UserController : Controller
    {
        //i'll look into how [Authorize] would work in this file, exactly
        //i want to prevent users that aren't logged in from accessing the dashboard through the url
        //if i've done this before then i forgot, i'm sorry
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var model = new User
            {
                FirstName = user.FirstName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
            };

            return View("~/Views/Accounts/Dashboard.cshtml", model);
        }
    }
}
