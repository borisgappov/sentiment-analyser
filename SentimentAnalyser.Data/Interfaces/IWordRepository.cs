using System.Threading.Tasks;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser.Data.Interfaces
{
    public interface IWordRepository : IGenericRepository<Word>
    {
        Task<bool> Exists(string text);
    }
}