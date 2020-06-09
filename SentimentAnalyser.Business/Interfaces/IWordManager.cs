using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using Microsoft.AspNetCore.Http;
using SentimentAnalyser.Models;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser.Business.Interfaces
{
    public interface IWordManager : IGenericManager<Word>
    {
        Task<bool> Exists(string text);

        Task<bool> AddAsync(Word model);

        Task<AnalyzeTextResponse> AnalyzeFile(IFormFile file);

        Task<AnalyzeTextResponse> AnalyzeText(AnalyzeTextRequest model);

        Task<LoadResult> DataSourceLoad(DataSourceLoadOptionsBase loadOptions);
    }
}