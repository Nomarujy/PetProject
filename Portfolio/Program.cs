using Microsoft.EntityFrameworkCore;
using Portfolio.Areas._7DTD.Data.BloodNightRepository;
using Portfolio.Data.Logger;
using Microsoft.AspNetCore.Routing.Constraints;
using Portfolio.Data.Database.Context;
using Portfolio.Data.Database.ContactService;

var builder = WebApplication.CreateBuilder(args);

#region Services
//builder.Logging.AddProvider(new TextLoggerProvider());

string connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(connectionString));


builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Cookies").AddCookie();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddSingleton<IBloodNightRepository, BloodNightRepository>();

#endregion Services

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(name: "default", 
    pattern: "{action}",
    new { controller = "Home", action = "Index" });

app.MapAreaControllerRoute(name: "7DaysToDie", "7DTD",
    pattern:"7DTD/{action}",
    new { controller = "BloodNight", Action = "Index" });

app.MapAreaControllerRoute(name: "News", "News", 
    pattern: "News/{controller}/{action}/{Id?}",
    new { controller = "Read", Action = "Index" },
    constraints: new {Id = new IntRouteConstraint() });

app.Run();
