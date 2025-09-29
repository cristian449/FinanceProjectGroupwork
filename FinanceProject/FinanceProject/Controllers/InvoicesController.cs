using FinanceProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FinanceProject.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly FinancesDbContext _context;

        public InvoicesController(FinancesDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices.ToListAsync();
            return View(invoices);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
