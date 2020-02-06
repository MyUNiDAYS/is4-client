# OIDC Client
A Sample .NET Core Client using razor pages for use with Open ID Connect (PKCE method).  



# Prerequisites  
* Visual Studio 2019 16.4 or later with the ASP.NET and web development workload  
* .NET Core [SDK version 3.1.1](https://aka.ms/dotnet-download)  
* Command line [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/) tools.  


# Quick Start with C# .NET  

To build a client from scratch, follow the steps below, it assumes you have the prerequisites already installed.  
If you'd prefer not to start from scratch then simply:  
* clone this repository  
* add your configuration (clientid, endpoints)  
* build  
* run

## Make a new MVC web application:  
`mkdir identity-client`  
`cd identity-client`  
`dotnet new webapp`  
## Add the package:  
`dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect`  
## Add the project to a solution (for ease of use in VisualStudio 2019):  
`cd ..`  
`dotnet new sln -n identity-client`  
`dotnet sln add ./identity-client/identity-client.csproj`  
 

 
 ## Change startup configuration (startup.cs):  

````
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace identity_client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddOpenIdConnect(options =>
                {
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.Authority = "https://demo.identityserver.io";  
                    options.RequireHttpsMetadata = true;
                    options.ClientId = "interactive.public";  //YOUR CLIENTID HERE
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.UsePkce = true;
                    options.Scope.Add("profile");
                    options.Scope.Add("offline_access");
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                });

            services.AddAuthorization();
            services.AddRazorPages();
                       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            //NOTE: UseAuthentication has to come before UseAuthorization, see https://docs.microsoft.com/en-us/aspnet/core/migration/22-to-30?view=aspnetcore-3.1&tabs=visual-studio
            app.UseAuthentication(); 
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
````

## Add ability to view claims to main page (index.cshtml)

````
@page
@model IndexModel
@{
    ViewData["Title"] = "Home page";
}

<div class="text-left">
    <h1 class="display-8">Welcome</h1>
        

    @if (User.Identity.IsAuthenticated)
{
    <h2>Authenticated</h2>
    <ul>
        @foreach (var claim in User.Claims)
        {
            <li><strong>@claim.Type</strong>: @claim.Value</li>
        }
    </ul>


     <a href="/logout">Logout</a>
}
else
{
    <h2>User Not Authenticated </h2>
}
</div>

````

## Force the user to authorize to view the main page (index.cshtml.cs)

````
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace identity_client.Pages
{
    [Authorize]
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
    }
}

````


## Add the Logout page 

````
@page
@model identity_client.LogoutModel
@{
    ViewData["Title"] = "Logout";
}

<h1>Logout</h1>
````

## Add the logout code (logout.cshtml.cs)

````

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace identity_client
{
    [Authorize]
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("/SignedOut");
        }
    }
}
````

Finally, Add a razor page for signedout.  



 
