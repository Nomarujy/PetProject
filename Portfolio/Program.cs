using Portfolio;

var builder = WebApplication.CreateBuilder(args);

builder.Configure();

var app = builder.Build();

app.UseForwardedHeaders();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "Areas",
    pattern: "{area}/{controller}/{action}",
    new { controller = "Home", action = "Index" });

app.MapControllerRoute(name: "default",
    pattern: "{controller}/{action}",
    new { controller = "Home", action = "Index" });

app.Run();
