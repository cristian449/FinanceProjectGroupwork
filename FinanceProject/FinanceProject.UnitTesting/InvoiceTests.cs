namespace FinanceProject.UnitTesting
{
    public class InvoiceTests : TestBase
    {
        [Fact]
        public async Task ShouldNot_()
        {
            //ülesseade
            //i got no services

            //läbiviimine
            //var result = await controller.Create(SomekindofDto);

            //kontrollimine
            //Assert.NotNull(result);
        }
        [Fact]
        public async Task ShouldNot_Guh()
        {
            //ülesseadistus
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse(""); //what did he do

            //teostus
            //await Svc<IHAVENOSERVICES>().DetailAsync(guid);

            Assert.NotEqual(wrongGuid, guid);
        }
        [Fact]
        public async Task Should_Guh()
        {
            //ülesseadistus
            Guid wrongGuid = Guid.NewGuid();
            Guid guid = Guid.Parse(""); //what did he do

            //teostus
            //await Svc<IHAVENOSERVICES>().DetailAsync(guid);

            Assert.NotEqual(wrongGuid, guid);
        }
    }
}