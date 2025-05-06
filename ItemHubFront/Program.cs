using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddHttpClient();

services.AddSingleton(x =>
{
    var cloudName = configuration["Cloudinary:CloudName"];
    var apiKey = configuration["Cloudinary:ApiKey"];
    var apiSecret = configuration["Cloudinary:ApiSecret"];
    var account = new Account(cloudName, apiKey, apiSecret);
    return new Cloudinary(account);
});

services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
