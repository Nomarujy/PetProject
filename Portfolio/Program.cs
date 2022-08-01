using Microsoft.EntityFrameworkCore;
using Portfolio.Areas._7DTD.Data.BloodNightRepository;
using Portfolio.Data.ContactService;
using Portfolio.Data.MainContext;
using Portfolio.Data.Logger;


var builder = WebApplication.CreateBuilder(args);

#region Services
builder.Logging.AddProvider(new TextLoggerProvider());

string connectionString = builder.Configuration.GetConnectionString("Database");
builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseSqlServer(connectionString));


builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddSingleton<IBloodNightRepository, BloodNightRepository>();

#endregion Services

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();


app.MapControllerRoute(name: "default", pattern: "{action}", new { controller = "Home", action = "Index" });

app.MapAreaControllerRoute(name: "7DaysToDie", "7DTD", "7DTD/{action}", new { controller = "BloodNight", Action = "Index" });

app.MapAreaControllerRoute(name: "News", "News", "News/{controller}/{action}", new { controller = "Read", Action = "Index" });

app.Run();
