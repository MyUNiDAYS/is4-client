# IdentityServer Client
A Sample .NET Core Client for use with IdentityServer.  
This sample uses a gitbash command line [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x) approach with [SDK version 3.1.1](https://aka.ms/dotnet-download)    


# Quick Start with C# .NET

## Get the CLI templates
`dotnet new -i IdentityServer4.Templates`

## Make a new MVC web application:  
`mkdir is4-client`  
`cd is4-client`  
`dotnet new webapp`  
## Add the package:  
`dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect`  
## Add the project to a solution (for ease of use in VisualStudio):
`cd ..`  
`dotnet new sln -n is4-client`  
`dotnet sln add ./is4-client/is4-client.csproj`  
 
 ## Change startup configuration:  
 
````
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


````
 
 ## Add a button, action, and controller to invoke:
 
 ```
 controller, cshtml, etc.
 ```
 
Full working sample application is included in `is4-client/` subfolder
