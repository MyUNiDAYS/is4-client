# IdentityServer Client
A Sample .NET Core Client for use with IdentityServer.  
This sample uses a gitbash command line [.NET Core CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/) approach with [SDK version 3.1.1](https://aka.ms/dotnet-download)    

To build a client from scratch, follow the quick start guide below, it assumes you have .NET Core SDK 3.1.1 already installed.  
If you'd prefer not to start from scratch then simply:  
* clone this repository  
* add your configuration (client, secret, endpoints)  
* build  
* run

# Quick Start with C# .NET  
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
 
 ## Change startup configuration (startup.cs):  
 
````
using Microsoft.AspNetCore.Builder;
...


````
 
 ## Change client secret and endpoint configuration:  
 
 ## Add a button, action, and controller to invoke the login process:
 
 ```
 controller, cshtml, etc.
 ```
 
Full working sample application is included in `identity-client/` subfolder
