using FinanceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinanceProject.Controllers
{

    //Might change Controller name later dependant on if any of my teammates use the same name
    public class AccountsController : Controller
    {

        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public IActionResult Register() => View();


        [HttpPost]
            public async Task<IActionResult> Register(RegisterViewmodel model)
            {

                if (ModelState.IsValid)
                {
                    if (model.Password != model.Confirmpassword)
                    {
                        ModelState.AddModelError("Confirmpassword", "Passwords do not match.");
                        return View(model);
                    }

                var user = new User 
                { 
                    UserName = model.Email, 
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    DateOfBirth = model.DateOfBirth,
                };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Dashboard", "User");

                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                }

                return View(model);


            }
        


        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewmodel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false
                );


                if (result.Succeeded)
                    return RedirectToAction("Dashboard", "User");

                ModelState.AddModelError("", "Invalid login attempt");
            }

            return View(model);
        }
           
       
    


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Back()
        {
            return RedirectToAction("Index", "Home");
        }


    }
}
