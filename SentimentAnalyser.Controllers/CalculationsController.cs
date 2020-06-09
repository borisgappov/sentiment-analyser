using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SentimentAnalyser.Business.Interfaces;
using SentimentAnalyser.Models;

namespace SentimentAnalyser.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class CalculationsController : ControllerBase
    {
        private readonly IWordManager _wordManager;

        public CalculationsController(IWordManager wordManager)
        {
            _wordManager = wordManager;
        }

        [HttpPost]
        [Route("AnalyzeFile")]
        public async Task<ActionResult<AnalyzeTextResponse>> AnalyzeFile(IFormFile file)
        {
            return await _wordManager.AnalyzeFile(file);
        }

        [HttpPost]
        [Route("AnalyzeText")]
        public async Task<ActionResult<AnalyzeTextResponse>> AnalyzeText([FromBody] AnalyzeTextRequest model)
        {
            return await _wordManager.AnalyzeText(model);
        }
    }
}