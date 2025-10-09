using FinanceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinanceProject.Data
{
    public class FinancesDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public FinancesDbContext(DbContextOptions<FinancesDbContext> options)
            : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }
    }
}
