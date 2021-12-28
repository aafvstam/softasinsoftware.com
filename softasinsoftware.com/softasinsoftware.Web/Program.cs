using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using softasinsoftware.Web;
using softasinsoftware.Web.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// todo take out hardcoded API URI
builder.Services.AddHttpClient("softasinsoftware.API", client => client.BaseAddress = new Uri("https://softasinsoftwareapi.azurewebsites.net/"));
// builder.Services.AddHttpClient("softasinsoftware.API", client => client.BaseAddress = new Uri("https://localhost:7067/"));

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
});

await builder.Build().RunAsync();
