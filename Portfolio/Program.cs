using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Portfolio.Areas;
using Portfolio.Data;
using Portfolio.Utilites;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.AddLogerProviders();

string connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDatabase(connectionString);
builder.Services.AddMyServices();
builder.Services.AddAreaServices();


builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Cookies").AddCookie();
builder.Services.AddAuthorization();

#endregion Services

var app = builder.Build();

#region Midleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

#endregion Midleware

app.MapControllerRoute(name: "default",
    pattern: "{controller}/{action}",
    new { controller = "Home", action = "Index" });

app.MapAreaControllerRoute(name: "7DaysToDie", "7DTD",
    pattern: "7DTD/{action}",
    new { controller = "BloodNight", Action = "Index" });

app.MapAreaControllerRoute(name: "News", "News",
    pattern: "News/{controller}/{action}/{Id?}",
    new { controller = "Read", Action = "Index" },
    constraints: new { Id = new IntRouteConstraint() });

app.Run();
