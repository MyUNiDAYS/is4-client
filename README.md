# IdentityServer Client
A Sample .NET Core Client for use with IdentityServer.  
This sample uses a gitbash command line [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/?tabs=netcore2x) approach with [SDK version 3.1.1](https://aka.ms/dotnet-download)    


# Quick Start with C# .NET  
To build a client from scratch, follow this quick start guide.  
Assumes you have .NET Core SDK 3.1.1 already installed.  

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
 
 ## Change startup configuration:  
 
````
using Microsoft.AspNetCore.Builder;
...


````
 
 ## Add a button, action, and controller to invoke:
 
 ```
 controller, cshtml, etc.
 ```
 
Full working sample application is included in `identity-client/` subfolder
