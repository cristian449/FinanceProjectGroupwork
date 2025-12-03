using FinanceProject.Controllers;
using FinanceProject.Data;
using FinanceProject.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Tarpe23CristianTestingXUnit
{
    public class InvoiceTest : TestBase
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

            //This makes sure we redirect back to index, making these tests complicated as i dont 
            //Really have any other backend test to make as I dont have services nor do the Controllers have much logic
            //Anyways once again this makes sure we redirect back to index
            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);

            // Checks if the invoice was actually added to the database
            var saved = context.Invoices.FirstOrDefault(i => i.Header == "Test Invoice");
            Assert.NotNull(saved);
            Assert.Equal(100, saved.Amount);
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
                Header = "Test Invoice Delete",
                Description = "Unit Test Delete",
                Amount = 100,
                InvoiceCategory = InvoiceCategories.Food,
                InvoiceDate = DateTime.Now
            };

            await controller.Create(testInvoice);

            var savedInvoice = context.Invoices.First(i => i.Header == "Test Invoice Delete");
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

        [Fact]
        public async Task Should_ReturnInvoiceList_OnIndex()
        {
            var controller = Svc<InvoicesController>();
            var context = Svc<FinancesDbContext>();


            context.Invoices.Add(new Invoice
            {
                Header = "Index Test Invoice",
                Description = "Just for Index test, plz work",
                Amount = 50,
                InvoiceCategory = InvoiceCategories.MISC,
                InvoiceDate = DateTime.Now
            });
            await context.SaveChangesAsync();

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Invoice>>(viewResult.Model);

            Assert.Contains(model, i => i.Header == "Index Test Invoice");
        }


        [Fact]
        public async Task Should_ReturnDetailsView_WhenInvoiceExists()
        {
            var controller = Svc<InvoicesController>();
            var context = Svc<FinancesDbContext>();

            var inv = new Invoice
            {
                Header = "Details Invoice",
                Description = "Details test",
                Amount = 10,
                InvoiceCategory = InvoiceCategories.Food,
                InvoiceDate = DateTime.Now
            };
            context.Invoices.Add(inv);
            await context.SaveChangesAsync();

            var saved = context.Invoices.First(i => i.Header == "Details Invoice");

            var result = await controller.Details(saved.InvoiceID);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Invoice>(viewResult.Model);

            Assert.Equal(saved.InvoiceID, model.InvoiceID);
            Assert.Equal("Details Invoice", model.Header);
        }

        //GPT link chat ling about how to bloody to Edit https://chatgpt.com/share/69307a26-e870-8013-bb7b-756b74760995


        //No Clue how doing this one, I tried in vain for a while to get it to work
        //However i Have concluded that it is impossible! Unless I remake the EditPost method completely
        //However since i am not the one who made the controller nor made the methods I will not do it as that 
        //Is not my job to change other peoples code
        // I did find a way to do it however it is not clean, nor is it 100% a Unittest anymore more of an integration test
        //Cause of that i have decided not to do it

        //[Fact]
        //public async Task Should_UpdateInvoice_OnUpdateMethodCall()
        //{
        //    var controller = Svc<InvoicesController>();
        //    var context = Svc<FinancesDbContext>();

        //    var guid = new Guid(4);


        //    Invoice invoice = new()
        //    {
        //        Header = "Invoice"
        //        Description = "Unit Test",
        //        Amount = 100,
        //        InvoiceCategory = InvoiceCategories.Food,
        //        InvoiceDate = DateTime.Now
        //    };
        //    Invoice domain = new()

        //    domain.InvoiceID = 4;
        //    domain.Header = "Updated Invoice";
        //    domain.Description = "Updated Description";
        //    domain.InvoiceCategory = InvoiceCategories.Travel;
        //    domain.InvoiceDate = DateTime.MaxValue;

        //    await Svc<InvoicesController>().EditPost(invoice);

        //    Assert.Equal(domain.InvoiceID, int)
        //    Assert.NotEqual(domain.Header, invoice.Header);
        //    Assert.NotEqual(domain.Description, invoice.Description);
        //    Assert.NotEqual(domain.InvoiceCategory, invoice.InvoiceCategory);
        //    Assert.DoesNotMatch(domain.InvoiceDate, invoice.InvoiceDate);

        //}

        // Same as upper one but negative

        //[Fact]
        //public async Task ShouldNot_UpdateInvoice_WhenDataNull()


        [Fact]
        public async Task Should_CreateInvoice_WhenAmountNegative()
        {
            var controller = Svc<InvoicesController>();
            var context = Svc<FinancesDbContext>();

            Invoice testInvoice = new Invoice
            {
                Header = "Negative Amount Invoice",
                Amount = -1241212111,
                Description = "Negative Sum Test",
                InvoiceCategory = InvoiceCategories.MISC,
                InvoiceDate = DateTime.Now
            };

            var result = await controller.Create(testInvoice);
            Assert.NotNull(result);

            var saved = context.Invoices.FirstOrDefault(i => i.Header == "Negative Amount Invoice");
            Assert.NotNull(saved);
            Assert.Equal(-1241212111, saved.Amount);
        }




        [Fact]
        public async Task Should_Handle_NullFieldsANDNOTCRASH_OnCreate()
        {
            var controller = Svc<InvoicesController>();
            var context = Svc<FinancesDbContext>();

            Invoice testInvoice = new()
            {
                Header = null,         
                Description = null,
                Amount = 0,
                InvoiceCategory = 0,
                InvoiceDate = DateTime.MinValue
            };

            var result = await controller.Create(testInvoice);

  
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Invoice>(viewResult.Model);


            Assert.False(controller.ModelState.IsValid);

            // Should NOT! save to database
            var saved = context.Invoices.FirstOrDefault(i => i.Amount == 0);
            Assert.Null(saved);
        }



        //Same shite as i said in upper one about Edit, cant do update tests unless i remake the EditPost method or make it an integration test
        //Then again I am able to make some scuff tests for Editpost however they wouldnt be testing the method exactly
        //And so they would be kinda useless completely.
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


        //Next 2 tests are the only ones I could come up with that somewhat test EditPost method
        //Very scuff tests but hey better than nothing
        // I hate UnitTesting so much more than UserInterface module tesing, this pain tops CreateInvoice testing

        [Fact]
        public async Task Should_ReturnView_OnEditPost_WhenNoDataProvided()
        {
            var controller = Svc<InvoicesController>();
            var context = Svc<FinancesDbContext>();

            var inv = new Invoice
            {
                Header = "Pain Header",
                Description = "Painful Desc",
                Amount = 10,
                InvoiceCategory = InvoiceCategories.Food,
                InvoiceDate = DateTime.Now
            };

            context.Invoices.Add(inv);
            await context.SaveChangesAsync();

            var savedBefore = context.Invoices.First(i => i.Header == "Pain Header");
            int id = savedBefore.InvoiceID;

            await Assert.ThrowsAsync<ArgumentNullException>(() => controller.EditPost(id));

            var savedAfter = context.Invoices.First(i => i.InvoiceID == id);

            Assert.Equal("Pain Header", savedAfter.Header);
            Assert.Equal("Painful Desc", savedAfter.Description);
            Assert.Equal(10, savedAfter.Amount);
        }


        [Fact]
        public async Task Should_ReturnNotFound_OnEditPost_WhenIdNull()
        {
            var controller = Svc<InvoicesController>();

            var result = await controller.EditPost(null);

            Assert.IsType<NotFoundResult>(result);
        }


    }


}
