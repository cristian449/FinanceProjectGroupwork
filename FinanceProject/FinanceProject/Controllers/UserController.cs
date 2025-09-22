using Microsoft.AspNetCore.Mvc;

namespace FinanceProject.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
