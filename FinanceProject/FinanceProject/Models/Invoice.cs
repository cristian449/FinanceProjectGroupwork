using System.ComponentModel.DataAnnotations;

namespace FinanceProject.Models
{
    public enum InvoiceCategories
    {
        Food, Travel, idk, creature, MISC
    }

    public class Invoice
    {
        public int InvoiceID { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Header { get; set; } // aka title
        [StringLength(200)]
        public string? Description { get; set; }
        public float Amount { get; set; } // money
        public InvoiceCategories InvoiceCategory { get; set; }
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

    }
}
