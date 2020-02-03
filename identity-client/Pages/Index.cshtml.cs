using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using IdentityModel.Client;

namespace identity_client.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var serverEndpoint = SampleConfig.ServerEndpoint;
            var redirectUrl = SampleConfig.RedirectEndpoint;

            //1. Discovery
            _logger.LogInformation("Discovery step: begin");
            var client = new HttpClient();
            var discovery = await client.GetDiscoveryDocumentAsync(serverEndpoint);
            if (discovery.IsError)
            {
                _logger.LogError(discovery.Error.ToString());
                throw new Exception($"discovery error '{discovery.Error.ToString()}' on endpoint '{serverEndpoint}'");
            }

            _logger.LogInformation("Discovery step: complete");
            
            var state = Guid.NewGuid().ToString().Substring(0, 10); 
            var ru = new RequestUrl(discovery.AuthorizeEndpoint);
            var url = ru.CreateAuthorizeUrl(SampleConfig.ClientId, SampleConfig.ResponseType, SampleConfig.Scopes, redirectUrl, state);

            _logger.LogInformation("Redirecting to authorization Url");
            return Redirect(url);
        }
    }
}
