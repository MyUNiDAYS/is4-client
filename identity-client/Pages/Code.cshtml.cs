using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;

namespace identity_client.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class CodeModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public CodeModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync([FromQuery(Name = "code")]string code, [FromQuery(Name = "state")] string state)
        {
            //2. Authorize
            _logger.LogInformation("Authorize step: begin");
            var client = new HttpClient();
            var response = await client.RequestAuthorizationCodeTokenAsync(
                new AuthorizationCodeTokenRequest
                {
                    Address = SampleConfig.TokenEndpoint,
                    Code = code,
                    ClientId = SampleConfig.ClientId,
                    ClientSecret = SampleConfig.ClientSecret,
                    RedirectUri = SampleConfig.RedirectEndpoint
                });

            _logger.LogInformation("Authorize step: complete");

            //3. UserInfo
            _logger.LogInformation("UserInfo step: begin");
            var userInfo = await client.GetUserInfoAsync(
                new UserInfoRequest
                {
                    Address = SampleConfig.UserInfoEndpoint,
                    ClientId = SampleConfig.ClientId,
                    ClientSecret = SampleConfig.ClientSecret,
                    Token = response.AccessToken,
                });

            _logger.LogInformation("UserInfo step: complete");
            _logger.LogInformation($"UserInfo details: {userInfo.Json.ToString()}");
            _logger.LogInformation("Redirecting to private page");

            return RedirectToPage("Privacy"); //proceed to our protected page
        }
    }
}