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

                var user = new User { UserName = model.Email, Email = model.Email };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {

                        await _signInManager.SignInAsync(user, isPersistent: false);

                        return RedirectToAction("Index", "Home");

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
                {
                    if (!string.IsNullOrEmpty(model.AdminKey))
                    {
                        var user = await _userManager.FindByEmailAsync(model.Email);
                        if (user != null)
                        {
                            var claims = await _userManager.GetClaimsAsync(user);
                            var adminKeyClaim = claims.FirstOrDefault(c => c.Type == "AdminKey");

                            if (adminKeyClaim != null && adminKeyClaim.Value == model.AdminKey)
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                ModelState.AddModelError("AdminKey", "Invalid admin key");
                                await _signInManager.SignOutAsync();
                                return View(model);
                            }
                        }
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user == null)
                    {
                        ModelState.AddModelError("Email", "User not found");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Invalid password");
                    }
                }
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
