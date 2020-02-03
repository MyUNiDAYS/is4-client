# IdentityServer Client
A Sample .NET Core Client using razor pages for use with IdentityServer.  


# Prerequisites  
* Visual Studio 2019 16.4 or later with the ASP.NET and web development workload  
* .NET Core [SDK version 3.1.1](https://aka.ms/dotnet-download)  
* Command line [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/) tools.  


# Quick Start with C# .NET  

To build a client from scratch, follow the steps below, it assumes you have the prerequisites already installed.  
If you'd prefer not to start from scratch then simply:  
* clone this repository  
* add your configuration (client, secret, endpoints)  
* build  
* run


## Get the CLI templates  
`dotnet new -i IdentityServer4.Templates`

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
 
 
 ## Change client secret and endpoint configuration:  
 
 ```
     public static class SampleConfig
    {
        //Server config
        public const string ServerEndpoint = "http://localhost:10051";
        public const string TokenEndpoint = ServerEndpoint + "/connect/token";
        public const string UserInfoEndpoint = ServerEndpoint + "/connect/userinfo";

        //Client(this) Config
        public const string LaunchUrl = "http://localhost:49643";  
        public const string RedirectEndpoint = LaunchUrl + "/Code";  
        public const string ClientId = "client1";
        public const string ClientSecret = "secret";
        public const string Scopes = "openid profile email";
        public const string ResponseType = "code";
    }
 ```
 
 ## Change startup configuration (startup.cs):  
 
1. Use JWT Tokens and remove automatic authentication:  

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(c => { c.AutomaticAuthentication = false; });
            services.AddRazorPages();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = SampleConfig.ServerEndpoint;
                    options.RequireHttpsMetadata = false;

                    options.ClientId = SampleConfig.ClientId;
                    options.ClientSecret = SampleConfig.ClientSecret;
                    options.ResponseType = SampleConfig.ResponseType;

                    options.SaveTokens = true;
                });
        }

2. Use Authentication in the application builder  

```
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{  
...  
   app.UseAuthentication();   
...  
}  
```  
 
 
 ## Add pages and actions to invoke the login process:
 
 ```
 index.cshtml  
 index.cshtml.cs  
 code.cshtml  
 code.cshtml.cs  
 privacy.cshtml  
 privacy.cshtml.cs  
  ```
 
## Viewing Results:  
 
The sample uses the default Microsoft ILogger Logging Extension  
The default ASP.NET Core project templates call CreateDefaultBuilder, which adds the following logging providers:

* Console  
* Debug  
* EventSource  
* EventLog (only when running on Windows)

 
