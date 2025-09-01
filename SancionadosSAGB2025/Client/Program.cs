using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SancionadosSAGB2025.Client;
using SancionadosSAGB2025.Client.Services;
using SancionadosSAGB2025.Client.Shared.Extensiones;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("SancionadosSAGB2025.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();
// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("SancionadosSAGB2025.ServerAPI"));
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<FaltasSPGService>();
builder.Services.AddScoped<CatalagosServiceClient>();
builder.Services.AddScoped<FaltasSPNoGravesService>();
builder.Services.AddScoped<FaltasGravesPersonasFisicasService>();
//builder.Services.AddOptions();
//builder.Services.AddScoped<AuthenticationStateProvider, AutenticacionExtension>();
//builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
