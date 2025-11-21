using FinanceProject.Controllers;
using FinanceProject.Data;
using FinanceProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceProject.UnitTesting
{
    public class InvoiceTests : TestBase
    {
        [Fact]
        public async Task Should_AddInvoice_WhenModelIsValid()
        {
            var controller = Svc<InvoicesController>();
            var context = Svc<FinancesDbContext>();

            Invoice testInvoice = new()
            {
                Header = "Test Invoice",
                Description = "Unit Test",
                Amount = 100,
                InvoiceCategory = InvoiceCategories.Food,
                InvoiceDate = DateTime.Now
            };

            var result = await controller.Create(testInvoice);


            Assert.NotNull(result);

        }

        [Fact]
        public async Task ShouldNot_GetByIdInvoice_WhenReturnIsNotEqual()
        {
            var controller = Svc<InvoicesController>();

            int wrongId = 999;

            var result = await controller.Details(wrongId);

            Assert.IsType<NotFoundResult>(result);

        }

        [Fact]
        public async Task Should_DelettByIdInvoice_WhenDeleteInvoice()
        {
            var controller = Svc<InvoicesController>();
            var context = Svc<FinancesDbContext>();

            Invoice testInvoice = new()
            {
                Header = "Test Invoice",
                Description = "Unit Test",
                Amount = 100,
                InvoiceCategory = InvoiceCategories.Food,
                InvoiceDate = DateTime.Now
            };

            await controller.Create(testInvoice);

            var savedInvoice = context.Invoices.First(i => i.Header == "Test Invoice");
            int invoiceId = savedInvoice.InvoiceID;

            var result = await controller.DeleteConfirmed(invoiceId);

            Assert.IsType<RedirectToActionResult>(result);

            var deleted = context.Invoices.FirstOrDefault(i => i.InvoiceID == invoiceId);
            Assert.Null(deleted);
        }

        [Fact]
        public async Task Should_NotDelete_OtherInvoice_WhenDeletingSpecificInvoice()
        {

            var controller = Svc<InvoicesController>();
            var context = Svc<FinancesDbContext>();


            Invoice invoice1 = new()
            {
                Header = "Invoice 1",
                Description = "Unit Test 1",
                Amount = 100,
                InvoiceCategory = InvoiceCategories.Food,
                InvoiceDate = DateTime.Now
            };

            Invoice invoice2 = new()
            {
                Header = "Invoice 2",
                Description = "Unit Test 2",
                Amount = 200,
                InvoiceCategory = InvoiceCategories.Food,
                InvoiceDate = DateTime.Now
            };

            await controller.Create(invoice1);
            await controller.Create(invoice2);

            var saved1 = context.Invoices.First(i => i.Header == "Invoice 1");
            var saved2 = context.Invoices.First(i => i.Header == "Invoice 2");

            int id1 = saved1.InvoiceID;
            int id2 = saved2.InvoiceID;

            await controller.DeleteConfirmed(id2);


            var stillExists = context.Invoices.FirstOrDefault(i => i.InvoiceID == id1);
            var deleted = context.Invoices.FirstOrDefault(i => i.InvoiceID == id2);

            Assert.NotNull(stillExists);
            Assert.Null(deleted);
        }


        //Clue

        [Fact]
        public async Task Should_UpdateInvoice_OnUpdateMethodCall()
        {
            var controller = Svc<InvoicesController>();
            var context = Svc<FinancesDbContext>();

            var guid = new Guid("12121212-2323-2323-2323-123412341234");


            Invoice invoice = new()
            {
                Header = "Invoice",
                Description = "Unit Test",
                Amount = 100,
                InvoiceCategory = InvoiceCategories.Food,
                InvoiceDate = DateTime.Now
            };
            Invoice domain = new();

            domain.InvoiceID = 4;
            domain.Header = "Updated Invoice";
            domain.Description = "Updated Description";
            domain.InvoiceCategory = InvoiceCategories.Travel;
            domain.InvoiceDate = DateTime.MaxValue;

            await controller.EditPost(domain.InvoiceID);

            Assert.Equal(domain.InvoiceID, invoice.InvoiceID);
            Assert.NotEqual(domain.Header, invoice.Header);
            Assert.NotEqual(domain.Description, invoice.Description);
            Assert.NotEqual(domain.InvoiceCategory, invoice.InvoiceCategory);
            Assert.DoesNotMatch(domain.InvoiceDate.ToString(), invoice.InvoiceDate.ToString());

        }

        // Same as upper one but negative

        //[Fact]
        //public async Task ShouldNot_UpdateInvoice_WhenDataNull()


        //No idea if work
        [Fact]
        public async Task Should_CreateInvoice_WhenAreaNegative()
        {
            Invoice testInvoice = new Invoice
            {
                Amount = -1241212111,
                Description = "Negative Sum Test",
            };

            var result = await Svc<InvoicesController>().Create(testInvoice);
            Assert.NotNull(result);

            Assert.Equal(-1241212111, testInvoice.Amount);

        }



        //KILL ME PLZ, OH GOD WHY, IS THIS WHAT HELL FEELS LIKE, TRULY TORTUROUS EXISTENCE


        [Fact]
        public async Task ShouldNotAdd_NullInvoice()
        {
            var controller = Svc<InvoicesController>();

            Invoice testInvoice = new()
            {
                Header = null,
                Description = null,
                Amount = 0,
                InvoiceCategory = 0,
                InvoiceDate = DateTime.MinValue
            };


            var result = await Svc<InvoicesController>().Create(testInvoice);

            Assert.NotNull(result);



        }


        //[Fact]
        //public async Task ShouldUpdate_WithCorrectDataType()
        //{
        //    var controller = Svc<InvoicesController>();


        //    Invoice testInvoice = new()
        //    {
        //        Header = null,
        //        Description = null,
        //        Amount = 0,
        //        InvoiceCategory = 0,
        //        InvoiceDate = DateTime.MinValue
        //    };


        //    testInvoice.Amount = 50;


        //    var result = await Svc<InvoicesController>().Create(testInvoice);

        //    Assert.IsType<int>(result.Amount);





        //} 




        //No idea if work
        private static Invoice MockInvoice()
        {
            return new()
            {
                Header = "Mock Invoice",
                Description = "This is a mock invoice for testing.",
                Amount = 150.75f,
                InvoiceCategory = InvoiceCategories.MISC,
                InvoiceDate = DateTime.Now

                //Create same thing as MockInvoice but make things null can call it MockNullInvoicew NO IDea though if stuff nullable to yayayaya
            };
        }


    }


}