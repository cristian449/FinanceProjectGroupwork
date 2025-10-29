using FinanceProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1;
using System;

namespace FinanceProject.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(FinancesDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>("Admin"));
            }

            if (!context.Invoices.Any())
            {
                var invoices = new Invoice[]
                {
                    new Invoice {Header="Pancake", Description="Oooooh it was sooo good mmm", Amount=10, InvoiceCategory=InvoiceCategories.Food, InvoiceDate=DateTime.Now},
                    new Invoice {Header="Worm", Description="...", Amount=100, InvoiceCategory=InvoiceCategories.creature, InvoiceDate=DateTime.Now},
                    new Invoice {Header="Big bag", Description="there was stuff in it", Amount=50, InvoiceCategory=InvoiceCategories.MISC, InvoiceDate=DateTime.Now},
                };
                context.Invoices.AddRange(invoices);
                await context.SaveChangesAsync();
            }
            var AdminFirstName = "CALAMARIA";
            var AdminLastName = "OOGABOOGA";
            var AdminCreate = DateTime.Now;
            var adminEmail = "TestAdmin@gmail.com";
            var adminKey = "Admin";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    FirstName = AdminFirstName,
                    LastName = AdminLastName,
                    CreatedAt = AdminCreate,
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var createResult = await userManager.CreateAsync(adminUser, "Admin@123");

                if (createResult.Succeeded)
                {
                    await userManager.AddClaimAsync(adminUser, new System.Security.Claims.Claim("AdminKey", adminKey));
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }
        }
    }
}