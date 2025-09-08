using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using SancionadosSAGB2025.Server.Services;
using System.Buffers.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<FaltasServidoresPublicosGService>();
builder.Services.AddScoped<CatalogosService>();
builder.Services.AddScoped<DatosGeneralesService>();

var baseUrl = builder.Configuration["UrlApi:test"];

builder.Services.AddHttpClient<FaltasServidoresPublicosGService>(client => { client.BaseAddress = new Uri(baseUrl!); });
builder.Services.AddHttpClient<DatosGeneralesService>(client => {	client.BaseAddress = new Uri(baseUrl!);});
builder.Services.AddHttpClient<CatalogosService>(client => {	client.BaseAddress = new Uri(baseUrl!);});
builder.Services.AddHttpClient<FaltasServidoresPublicosNoGravesService>(client => {	client.BaseAddress = new Uri(baseUrl!);});
builder.Services.AddHttpClient<FaltasGravesPersonasFisicasService>(client => {	client.BaseAddress = new Uri(baseUrl!);});
builder.Services.AddHttpClient<FaltasGravesPersonasMoralesService>(client => {	client.BaseAddress = new Uri(baseUrl!);});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
