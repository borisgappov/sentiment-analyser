using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SentimentAnalyser.Data;
using SentimentAnalyser.Models.UnitTests.Fakes;

namespace SentimentAnalyzer.Data.UnitTests
{
    public class BaseRepositoryUnitTests
    {
        public BaseRepositoryUnitTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDb" + Helper.FakeId()));
            services.Configure<OperationalStoreOptions>(x => { });
            TestDbContext = services.BuildServiceProvider().GetService<ApplicationDbContext>();
        }

        internal ApplicationDbContext TestDbContext { get; set; }
    }
}