using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentimentAnalyser.Business.Interfaces;
using SentimentAnalyser.Models;
using SentimentAnalyser.Models.Converters;

namespace SentimentAnalyser.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly IWordManager _wordManager;

        public WordsController(IWordManager wordManager)
        {
            _wordManager = wordManager;
        }

        /// <summary>
        ///     For remote operations
        ///     install devextreme.aspnet.taghelpers assebmly (need devextreme license)
        ///     and replace DataSourceLoadOptionsBase with DataSourceLoadOptions
        /// </summary>
        /// <param name="loadOptions"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<LoadResponse<WordModel>>> GetWords(
            [FromQuery] DataSourceLoadOptionsBase loadOptions
        )
        {
            return (await _wordManager.DataSourceLoadAsync(loadOptions)).ToResponse<WordModel>();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<WordModel>> PutWord([FromForm] int key, [FromForm] string values)
        {
            return (await _wordManager.UpdateAsync(key, values)).ToModel();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<WordModel>> PostWord([FromForm] string values)
        {
            return (await _wordManager.CreateAsync(values)).ToModel();
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteWord([FromForm] int key)
        {
            await _wordManager.DeleteAsync(key);
            return Ok();
        }
    }
}