using System;
using System.Threading.Tasks;
using SentimentAnalyser.Data.Interfaces;

namespace SentimentAnalyser.Data.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRepositoryMapper _repositoryTypeMapper;

        public UnitOfWork(ApplicationDbContext dbContext, IRepositoryMapper repositoryTypeMapper)
        {
            _dbContext = dbContext;
            _repositoryTypeMapper = repositoryTypeMapper;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public T CreateRepository<T>()
        {
            var type = _repositoryTypeMapper.GetImplementationType(typeof(T));
            if (type == null)
                throw new Exception($"Please map interface {typeof(T).FullName} to its implementation in Startup");
            return (T) Activator.CreateInstance(type, _dbContext);
        }

        public async Task<int> SaveAsync(int? userId)
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}