using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SentimentAnalyser.Data.Core;
using SentimentAnalyser.Data.Interfaces;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser.Data.Repositories
{
    public class WordRepository : GenericRepository<Word>, IWordRepository
    {
        private static readonly Func<ApplicationDbContext, string, Task<int>> _wordExists =
            EF.CompileAsyncQuery((ApplicationDbContext context, string text) =>
                context.Words.Count(x => x.Text == text));

        public WordRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> Exists(string text)
        {
            return await _wordExists(_context, text) > 0;
        }
    }
}