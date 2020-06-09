using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentimentAnalyser.Data.Interfaces
{
    public interface IGenericRepository<T>
    {
        void Attach(T model, EntityState? entityState = null);

        void Create(T model);

        void Update(T model);

        void Delete(T model);

        void Detach(T entity);

        Task<T> FindByIdAsync(int id);

        Task<List<T>> AllAsync();

        ApplicationDbContext GetContext();
    }
}
