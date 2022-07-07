global using Scan2XDocumentComparison.Server.Data;
global using Microsoft.EntityFrameworkCore;
global using Scan2XDocumentComparison.Shared;

global using Scan2XDocumentComparison.Server.Services.DocumentService;

using Microsoft.AspNetCore.ResponseCompression;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DocumentsContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDocumentService, DocumentService>();


var app = builder.Build();

app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapDefaultControllerRoute();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
