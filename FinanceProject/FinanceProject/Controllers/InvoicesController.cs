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

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FirstOrDefaultAsync(
                i => i.InvoiceID == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        public async Task<IActionResult> Create()
        {
            var invoices = await _context.Invoices.ToListAsync();
            return View(invoices);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create()
        //{
        //    var invoices = await _context.Invoices.ToListAsync();
        //    return View(invoices);
        //}
    }
}
