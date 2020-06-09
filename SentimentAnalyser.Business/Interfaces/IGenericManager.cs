using System.Collections.Generic;
using System.Threading.Tasks;

namespace SentimentAnalyser.Business.Interfaces
{
    public interface IGenericManager<T>
    {
        Task<T> CreateAsync(T entity);

        Task<T> CreateAsync(string values);

        Task UpdateAsync(T entity);

        Task<T> UpdateAsync(int id, string values);

        Task DeleteAsync(T entity);

        Task DeleteAsync(int id);

        Task<T> FindByIdAsync(int id);

        Task<List<T>> GetAllAsync();
    }
}
