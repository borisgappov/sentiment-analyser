using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SentimentAnalyser.Data;
using SentimentAnalyser.Models.Requests;

namespace SentimentAnalyser.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CalculationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("AnalyzeFile")]
        public async Task<ActionResult<string>> AnalyzeFile(IFormFile file)
        {
            return Ok();
        }

        [HttpPost]
        [Route("AnalyzeText")]
        public async Task<ActionResult<string>> AnalyzeText([FromBody]AnalyzeTextRequest model)
        {
            return Analyze(model.Text);
        }

        private string Analyze(string text)
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
                    lower = x.Trim().ToLower(),
                })
                .Where(x => !string.IsNullOrEmpty(x.text))
                .ToArray();

            // select database words
            var dbWords = _context.Words.ToArray();

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
                    
                    return result == newLine ? "<br/>" : result;
                }).ToArray();

            // combine to text
            var sb = new StringBuilder();
            foreach (var word in words)
            {
                if(sb.Length > 0) sb.Append(' ');
                sb.Append(word);
            }

            return sb.ToString();
        }
    }
}