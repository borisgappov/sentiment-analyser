using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentimentAnalyser.Data;
using SentimentAnalyser.Infrastructure.Extensions;
using SentimentAnalyser.Models;
using SentimentAnalyser.Models.Converters;
using SentimentAnalyser.Models.Entities;

namespace SentimentAnalyser.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class WordsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WordsController(ApplicationDbContext context)
        {
            _context = context;
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
            return (await DataSourceLoader.LoadAsync(_context.Words, loadOptions))
                .ToResponse<WordModel>();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<WordModel>> PutWord([FromForm] int key, [FromForm] string values)
        {
            var word = await _context.Words.FindAsync(key);
            values.Populate(word);
            _context.Entry(word).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordExists(key))
                    return NotFound();
                throw;
            }

            return word.ToModel();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<WordModel>> PostWord([FromForm] string values)
        {
            var word = values.Populate<Word>();
            _context.Words.Add(word);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WordExists(word.Id))
                    return Conflict();
                throw;
            }

            return word.ToModel();
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<WordModel>> DeleteWord([FromForm] int key)
        {
            var word = await _context.Words.FindAsync(key);
            if (word == null) return NotFound();

            _context.Words.Remove(word);
            await _context.SaveChangesAsync();

            return word.ToModel();
        }

        private bool WordExists(int id)
        {
            return _context.Words.Any(e => e.Id == id);
        }
    }
}