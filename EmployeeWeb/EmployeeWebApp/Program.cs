using EmployeeWebApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Services.BLL;
using Services.Common;
using Services.DAL;
using Services;
using EmployeeWebApp.Data;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
//  :given (This is code that is provided when we create our application)
//  supplied database connection due to the fact that we created this
//      web app to use Individual accounts
//  Core retrieves the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

//  :added
//  code retrieves the HogWild connection string
var connectionStringHogWild = builder.Configuration.GetConnectionString("WorkSchedule");

builder.Services.AddBackendDependencies(options =>
    options.UseSqlServer(connectionStringHogWild));

builder.Services.AddDbContext<EmployeeContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddAntDesign();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();



app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
