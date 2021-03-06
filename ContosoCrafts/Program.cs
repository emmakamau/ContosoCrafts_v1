using ContosoCrafts.services;
using ContosoCrafts.controllers;
using ContosoCrafts.WebSite.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//Add JsonFileProductService
builder.Services.AddTransient<JsonFileProductService>();
//Add API controller
builder.Services.AddControllers();
//Add Blazor components
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapBlazorHub();

//One way to return json data - API
/*app.MapGet("/products", context =>
{
    var products = app.Services.GetService<JsonFileProductService>().GetProducts();
    var json = JsonSerializer.Serialize<IEnumerable<Product>>(products);
    return context.Response.WriteAsync(json);
});*/

app.Run();
