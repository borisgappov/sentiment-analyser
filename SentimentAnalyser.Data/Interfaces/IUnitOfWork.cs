using System;
using System.Threading.Tasks;

namespace SentimentAnalyser.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();

        Task<int> SaveAsync();

        T CreateRepository<T>();
    }
}
