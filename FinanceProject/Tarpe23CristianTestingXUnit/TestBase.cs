using FinanceProject.Controllers;
using FinanceProject.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Tarpe23CristianTestingXUnit.Macros;
using Tarpe23CristianTestingXUnit.Mock;

namespace Tarpe23CristianTestingXUnit
{
    public abstract class TestBase
    {

            protected IServiceProvider serviceProvider { get; set; }

            protected TestBase()
            {
                var Services = new ServiceCollection();
                SetupServices(Services);
                serviceProvider = Services.BuildServiceProvider();
            }

            public virtual void SetupServices(IServiceCollection services)
            {
               // services.AddScoped<IHostingEnvironment, MockIHostEnvoirnment>(); Dont konw if need, test work without


            services.AddDbContext<FinancesDbContext>(x =>
                {
                    x.UseInMemoryDatabase("TestDb");
                    x.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                }
                );

                services.AddScoped<InvoicesController>();


            RegisterMacros(services);
            }

            public void dispose() { }

            protected T Svc<T>()
            {
                return serviceProvider.GetService<T>();
            }

            private void RegisterMacros(IServiceCollection services)
            {
                var macroBaseType = typeof(IMacros);
                var macros = macroBaseType.Assembly.GetTypes().Where(t => macroBaseType.IsAssignableFrom(t)
                && !t.IsInterface && !t.IsAbstract);

                foreach (var macro in macros) { services.AddSingleton(macro); }
            }




    }
    
}