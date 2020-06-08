using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;

namespace SentimentAnalyser.Controllers
{
    [ApiController]
    [Route("api/AntiForgeryToken")]
    public class AntiForgeryTokenController : ControllerBase
    {
        private readonly IAntiforgery _xsrf;

        public AntiForgeryTokenController(IAntiforgery xsrf)
        {
            _xsrf = xsrf;
        }


        [HttpGet]
        public ActionResult<string> Get()
        {
            return _xsrf.GetAndStoreTokens(ControllerContext.HttpContext).RequestToken;
        }
    }
}