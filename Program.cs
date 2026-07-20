using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UI;
using UI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiBaseUrl"] 
    ?? builder.Configuration["API_BASE_URL"]
    ?? Environment.GetEnvironmentVariable("ApiBaseUrl")
    ?? Environment.GetEnvironmentVariable("API_BASE_URL");

if (string.IsNullOrWhiteSpace(apiBaseUrl))
{
    apiBaseUrl = "http://localhost:5000/";
}

builder.Services.AddScoped(sp => new HttpClient 
{ 
    BaseAddress = new Uri(apiBaseUrl) 
});

builder.Services.AddSingleton<MockDataService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ApiService>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();
