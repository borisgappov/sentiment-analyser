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
        Task<bool> ExistsAsync(string text);

        Task<bool> AddAsync(Word model);

        Task<AnalyzeTextResponse> AnalyzeFileAsync(IFormFile file);

        Task<AnalyzeTextResponse> AnalyzeTextAsync(AnalyzeTextRequest model);

        Task<LoadResult> DataSourceLoadAsync(DataSourceLoadOptionsBase loadOptions);
    }
}