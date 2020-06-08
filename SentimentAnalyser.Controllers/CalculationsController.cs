using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentimentAnalyser.Data;
using SentimentAnalyser.Models;

namespace SentimentAnalyser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CalculationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AnalyzeFile")]
        public async Task<ActionResult<AnalyzeTextResponse>> AnalyzeFile(IFormFile file)
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

            return BadRequest();
        }

        [HttpPost]
        [Route("AnalyzeText")]
        public async Task<ActionResult<AnalyzeTextResponse>> AnalyzeText([FromBody] AnalyzeTextRequest model)
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
                .Where(x => !string.IsNullOrEmpty(x.text))
                .ToArray();

            // select database words
            var dbWords = await _context.Words.ToArrayAsync();

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