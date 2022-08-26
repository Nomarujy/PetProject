using Portfolio;

var builder = WebApplication.CreateBuilder(args);

builder.Configure();

var app = builder.Build();

app.Configure();

app.MapControllerRoute(name: "Areas",
    pattern: "{area}/{controller}/{action}",
    new { controller = "Home", action = "Index" });

app.MapControllerRoute(name: "default",
    pattern: "{controller}/{action}",
    new { controller = "Home", action = "Index" });

app.Run();
