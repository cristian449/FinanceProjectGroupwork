using System.ComponentModel.DataAnnotations;

namespace FinanceProject.Models
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Header { get; set; } // aka title
        [StringLength(200)]
        public string Description { get; set; }
        public float Amount { get; set; } // money
        public DateTime InvoiceDate { get; set; }

    }
}
