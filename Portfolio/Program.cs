using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Portfolio.Areas;
using Portfolio.Data;
using Portfolio.Utilites;
using Portfolio.Areas.News.Data.Post.Authorization;

var builder = WebApplication.CreateBuilder(args);

#region Services
builder.AddLogerProviders();

string connectionString = builder.Configuration.GetConnectionString("Database");

builder.Services.AddDatabase(connectionString);
builder.Services.AddRepository();
builder.Services.AddAreaServices();
builder.Services.AddPostAuthorizationHandlers();

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Cookies").AddCookie();
builder.Services.AddAuthorization(opt => { opt.AddPostPolitics(); });

#endregion Services

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(name: "7DaysToDie", "7DTD",
    pattern: "7DTD/{controller}/{action}",
    new { controller = "BloodNight", Action = "Index" });

app.MapAreaControllerRoute(name: "News", "News",
    pattern: "News/{controller}/{action}/{Id?}",
    new { controller = "Read", Action = "Index" },
    constraints: new { Id = new IntRouteConstraint() });

app.MapControllerRoute(name: "default",
    pattern: "{controller}/{action}",
    new { controller = "Home", action = "Index" });

app.Run();
