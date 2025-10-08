using FinanceProject.Data;
using FinanceProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Header, Description, Amount, InvoiceCategory")] Invoice invoice)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(invoice);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes, try again");
            }

            return View(invoice);
        }

        public async Task<IActionResult> Edit(int? id)
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
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var invoiceToUpdate = await _context.Invoices.FirstOrDefaultAsync(i => i.InvoiceID == id);
            if (await TryUpdateModelAsync<Invoice>(invoiceToUpdate, "",
                i => i.Header,
                i => i.Description,
                i => i.InvoiceCategory,
                i => i.Amount))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes" +
                    "Try again, and if the problem persists" +
                    "see your system administrator");
                }
            }
            return View(invoiceToUpdate);
        }
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
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

            //_context.Remove(invoice);
            //await _context.SaveChangesAsync();

            //if (saveChangesError.GetValueOrDefault())
            //{
            //    ViewData["ErrorMessage"] =
            //        "Delete failed. Try again, and if problem persists" +
            //        "see your system administrator";
            //}

            //return RedirectToAction(nameof(Index));
        }
    }
}
