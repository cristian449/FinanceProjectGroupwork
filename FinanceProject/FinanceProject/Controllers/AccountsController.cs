using FinanceProject.Models;
using FinanceProject.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace FinanceProject.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EmailsServices _emailService;
        private readonly UrlEncoder _urlEncoder;

        public AccountsController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            EmailsServices emailService,
            UrlEncoder urlEncoder)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _urlEncoder = urlEncoder;
        }

        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewmodel model)
        {
            if (ModelState.IsValid)
            {

                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    CreatedAt = DateTime.UtcNow,
                    UserName = model.Email,
                    Email = model.Email,
                    DateOfBirth = model.DateOfBirth,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action(
                        "VerifyEmail",
                        "Accounts",
                        new { userId = user.Id, token = token },
                        protocol: Request.Scheme);

                    var body = $@"
                        <h2>Welcome to FinanceProject!</h2>
                        <p>Please verify your email address by clicking the link below:</p>
                        <a href='{confirmationLink}'>Verify Email</a>
                        <p>This link will expire soon, so please confirm promptly.</p>";

                    try
                    {
                        _emailService.SendEmail(user.Email, "Confirm your email", body);
                        TempData["Message"] = "Registration successful! Please check your email to verify your account.";
                        return RedirectToAction("Login");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, $"Error sending verification email: {ex.Message}");
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId == null || token == null)
                return RedirectToAction("Index", "Home");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData["Message"] = "Email verified successfully! You can now log in.";
                return RedirectToAction("Login");
            }

            TempData["Message"] = "Invalid or expired verification link.";
            return RedirectToAction("Login");
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewmodel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "User not found.");
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("Email", "Please verify your email before logging in.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Password", "Invalid password.");
                return View(model);
            }

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                var adminClaim = (await _userManager.GetClaimsAsync(user))
                    .FirstOrDefault(c => c.Type == "AdminKey");

                if (adminClaim == null || string.IsNullOrWhiteSpace(model.AdminKey) || adminClaim.Value != model.AdminKey)
                {
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError("AdminKey", "Invalid or missing Admin Key.");
                    return View(model);
                }
            }


            user.IsActive = true;
            user.LastActive = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Dashboard", "User");
        }

        public async Task<IActionResult> Logout()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                user.IsActive = false;
                user.LastActive = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");


        }

        public IActionResult Back()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
