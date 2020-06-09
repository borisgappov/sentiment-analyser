using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using SentimentAnalyser.Business.Interfaces;
using SentimentAnalyser.Data.Interfaces;
using SentimentAnalyser.Models;
using SentimentAnalyser.Models.Entities;
using SentimentAnalyser.Utils;

namespace SentimentAnalyser.Business.Managers
{
    public class WordManager : GenericManager<Word>, IWordManager
    {
        public WordManager(IUnitOfWorkFactory uowFactory) : base(uowFactory)
        {
        }

        public async Task<LoadResult> DataSourceLoadAsync(DataSourceLoadOptionsBase loadOptions)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                return await DataSourceLoader.LoadAsync(
                    uow.CreateRepository<IWordRepository>().GetContext().Words,
                    loadOptions);
            }
        }


        public Task<bool> ExistsAsync(string text)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                return uow.CreateRepository<IWordRepository>().ExistsAsync(text);
            }
        }

        public async Task<bool> AddAsync(Word model)
        {
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                var wordRepository = uow.CreateRepository<IWordRepository>();
                if (await wordRepository.ExistsAsync(model.Text)) return false;

                wordRepository.Create(model);
                await uow.SaveAsync();
            }

            return true;
        }

        public async Task<AnalyzeTextResponse> AnalyzeFileAsync(IFormFile file)
        {
            if (file != null)
            {
                var result = new StringBuilder();
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                        result.AppendLine(await reader.ReadLineAsync());
                }

                return await Analyze(result.ToString());
            }

            return null;
        }

        public async Task<AnalyzeTextResponse> AnalyzeTextAsync(AnalyzeTextRequest model)
        {
            return await Analyze(model.Text);
        }

        private async Task<AnalyzeTextResponse> Analyze(string text)
        {
            var newLine = "{new-line}";

            // convert to text if html
            var doc = new HtmlDocument();
            doc.LoadHtml(text);
            text = doc.DocumentNode.InnerText;

            // split text into words
            var inputWords = text
                .Replace("\n", " " + newLine + " ")
                .Split(' ')
                .Select(x => new
                {
                    text = x.Trim(),
                    lower = x.Trim().ToLower()
                })
                .Where(x => x.text.IsNotEmpty())
                .ToArray();

            List<Word> dbWords = null;

            // select database words
            using (var uow = _uowFactory.CreateUnitOfWork())
            {
                dbWords = await uow.CreateRepository<IWordRepository>().AllAsync();
            }

            // mark positive and negative words
            var words =
                (from s in inputWords
                    from w in dbWords.Where(x => x.Text == s.lower).DefaultIfEmpty()
                    select new
                    {
                        s,
                        w
                    })
                .Select(x =>
                {
                    var result = x.w == null
                        ? x.s.text
                        : $"<span class=\"{(x.w.Sentiment > 0 ? "green" : "red")}\">{x.s.text}</span>";

                    return new
                    {
                        text = result == newLine ? "<br/>" : result,
                        sentiment = x.w?.Sentiment ?? 0
                    };
                }).ToArray();

            // combine to text
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if (sb.Length > 0) sb.Append(' ');
                sb.Append(word.text);
            }

            var sentiments = words.Where(x => x.sentiment != 0).Select(x => x.sentiment).ToArray();

            return new AnalyzeTextResponse
            {
                Html = sb.ToString(),
                Score = sentiments.Length == 0 ? 0 : sentiments.Average()
            };
        }
    }
}