using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SentimentAnalyser.Data.Interfaces;
using SentimentAnalyser.Infrastructure.Config;

namespace SentimentAnalyser.Data.Core
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ConnectionStringResolver _connectionStringResolver;
        private readonly IRepositoryMapper _repositoryTypeMapper;

        public UnitOfWorkFactory(
            ConnectionStringResolver connectionStringResolver,
            IRepositoryMapper repositoryTypeMapper
        )
        {
            _connectionStringResolver = connectionStringResolver;
            _repositoryTypeMapper = repositoryTypeMapper;
        }

        public IUnitOfWork CreateUnitOfWork()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_connectionStringResolver.GetConnectionString()));
            services.Configure<OperationalStoreOptions>(x => { });
            var context = services.BuildServiceProvider().GetService<ApplicationDbContext>();
            return new UnitOfWork(context, _repositoryTypeMapper);
        }
    }
}