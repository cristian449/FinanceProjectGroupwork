using Microsoft.AspNetCore.Mvc;

namespace FinanceProject.Controllers
{
    public class InvoicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
