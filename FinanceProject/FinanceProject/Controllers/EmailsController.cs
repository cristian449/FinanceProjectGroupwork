using FinanceProject.Models;
using FinanceProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text.Encodings.Web;

namespace FinanceProject.Controllers
{
    public class EmailsController : Controller
    {
        private readonly EmailsServices _emailService;
        private readonly UserManager<User> _userManager;
        private readonly UrlEncoder _urlEncoder;

        public EmailsController(EmailsServices emailService, UserManager<User> userManager, UrlEncoder urlEncoder)
        {
            _emailService = emailService;
            _userManager = userManager;
            _urlEncoder = urlEncoder;
        }

        // GET: /Emails/Email
        [HttpGet]
        public IActionResult Email()
        {
            return View(new EmailViewmodel());
        }

        // POST: /Emails/SendEmail
        [HttpPost]
        public IActionResult SendEmail(EmailViewmodel viewModel)
        {
            if (!ModelState.IsValid)
                return View("Email", viewModel);

            try
            {

                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

                // Creating a simple verification
                var verificationLink = Url.Action(
                    "Verify",
                    "Emails",
                    new { email = viewModel.To, token = token },
                    Request.Scheme);

                // Build email body, the body sent to the user
                var body = $@"
                    <h2>Email Verification</h2>
                    <p>Click the link below to verify your email address:</p>
                    <a href='{verificationLink}'>Verify Email</a>";

                // Send email 
                _emailService.SendEmail(viewModel.To, "Verify your email", body);

                ViewBag.Message = "Verification email sent successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error sending email: {ex.Message}";
            }

            return View("Email");
        }

        // GET: /Emails/Verify?email=...&token=...
        [HttpGet]
        public IActionResult Verify(string email, string token)
        {
            // For now, just show success/failure
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                ViewBag.Success = false;
                return View();
            }

            // Validate token from DB and mark user as verified
            ViewBag.Success = true;
            ViewBag.Email = email;
            return View();
        }
    }
}
