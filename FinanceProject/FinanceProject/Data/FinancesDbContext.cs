using FinanceProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceProject.Data
{
    public class FinancesDbContext : DbContext
    {
        public FinancesDbContext(DbContextOptions<FinancesDbContext> options)
        : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }
    }
}
