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
                new Invoice {Header="Pancake", Description="Oooooh it was sooo good mmm", Amount=10, InvoiceDate=DateTime.Now},
                new Invoice {Header="Worm", Description="...", Amount=100, InvoiceDate=DateTime.Now},
            };

            context.Invoices.AddRange(Invoices);
            context.SaveChanges();
        }

    }
}
