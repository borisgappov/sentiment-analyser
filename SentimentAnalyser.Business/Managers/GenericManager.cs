using System.Collections.Generic;
using System.Threading.Tasks;
using SentimentAnalyser.Business.Interfaces;
using SentimentAnalyser.Data.Interfaces;
using SentimentAnalyser.Utils;

namespace SentimentAnalyser.Business.Managers
{
    public class GenericManager<T> : IGenericManager<T> where T : class
    {
        protected readonly IUnitOfWorkFactory _uowFactory;

        public GenericManager(IUnitOfWorkFactory uowFactory)
        {
            _uowFactory = uowFactory;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                uow.CreateRepository<IGenericRepository<T>>().Create(entity);
                await uow.SaveAsync();
            }

            return entity;
        }

        public virtual async Task<T> CreateAsync(string values)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                var entity = values.Populate<T>();
                uow.CreateRepository<IGenericRepository<T>>().Create(entity);
                await uow.SaveAsync();
                return entity;
            }
        }

        public virtual async Task UpdateAsync(T entity)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                uow.CreateRepository<IGenericRepository<T>>().Update(entity);
                await uow.SaveAsync();
            }
        }

        public virtual async Task<T> UpdateAsync(int id, string values)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                var repository = uow.CreateRepository<IGenericRepository<T>>();
                var entity = await repository.FindByIdAsync(id);
                values.Populate(entity);
                repository.Update(entity);
                await uow.SaveAsync();
                return entity;
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                uow.CreateRepository<IGenericRepository<T>>().Delete(entity);
                await uow.SaveAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                var repository = uow.CreateRepository<IGenericRepository<T>>();
                var entity = await repository.FindByIdAsync(id);
                repository.Delete(entity);
                await uow.SaveAsync();
            }
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                return await uow.CreateRepository<IGenericRepository<T>>()
                    .FindByIdAsync(id);
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                return await uow.CreateRepository<IGenericRepository<T>>()
                        .AllAsync();
            }
        }
    }
}