using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Mvc;

namespace SentimentAnalyser.Controllers
{
    public class OidcConfigurationController : Controller
    {
        public OidcConfigurationController(IClientRequestParametersProvider clientRequestParametersProvider)
        {
            ClientRequestParametersProvider = clientRequestParametersProvider;
        }

        public IClientRequestParametersProvider ClientRequestParametersProvider { get; }

        /// <summary>
        ///     Use "SentimentAnalyser" as clientId
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("_configuration/{clientId}")]
        public IActionResult GetClientRequestParameters([FromRoute] string clientId)
        {
            return Ok(ClientRequestParametersProvider.GetClientParameters(HttpContext, clientId));
        }
    }
}