using Portfolio.Utilites;
using Portfolio;

var builder = WebApplication.CreateBuilder(args);
builder.AddLogerProviders();

builder.Services.AddControllersWithViews();
// Startup
builder.Services.AddDbAndAuthServices(builder.Configuration.GetConnectionString("PostgreSQL"));
builder.Services.AddLocalServices();  
builder.Services.AddAreaServices();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "Areas",
    pattern:"{area}/{controller}/{action}");

app.MapControllerRoute(name: "default",
    pattern: "{controller}/{action}",
    new { controller = "Home", action = "Index" });

app.Run();
