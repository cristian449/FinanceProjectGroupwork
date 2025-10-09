using FinanceProject.Models;

namespace FinanceProject.Data
{
    public static class DbInitializer
    {
        public static void Initialize(FinancesDbContext context) 
        {
            if (context.Invoices.Any())
            {
                return;
            }

            var Invoices = new Invoice[]
            {
                new Invoice {Header="Pancake", Description="Oooooh it was sooo good mmm", Amount=10, InvoiceCategory=InvoiceCategories.Food, InvoiceDate=DateTime.Now},
                new Invoice {Header="Worm", Description="...", Amount=100, InvoiceCategory=InvoiceCategories.creature, InvoiceDate=DateTime.Now},
                new Invoice {Header="Big bag", Description="there was stuff in it", Amount=50, InvoiceCategory=InvoiceCategories.MISC, InvoiceDate=DateTime.Now},
            };

            context.Invoices.AddRange(Invoices);
            context.SaveChanges();
        }

    }
}
