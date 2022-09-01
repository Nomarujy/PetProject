using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Portfolio.Areas._7DTD.Services;
using Portfolio.Areas.HelloKafka.Services.Kafka;
using Portfolio.Areas.News.Services.Authorization;
using Portfolio.Models;
using Portfolio.Models.Authentication.Entity;
using Portfolio.Services.Repository;
using Portfolio.Utilites;

namespace Portfolio
{
    public static class Startup
    {
        #region BuilderConfigure
        public static void Configure(this WebApplicationBuilder builder)
        {
            builder.AddLogerProviders();
            builder.Services.AddControllersWithViews();
            builder.Services.Configure<ForwardedHeadersOptions>(opt =>
            {
                opt.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            builder.Services.AddDbContext<ApplicationDbContext>();

            builder.Services.AddAuthentication("Cookies").AddCookie();
            builder.Services.AddAuthorization(opt => opt.AddPolitics());
            builder.Services.AddIdentity<User, Role>(opt => opt.Password.RequireNonAlphanumeric = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddPoliticsHandlers();

            builder.Services.AddServices();
        }

        private static void AddPolitics(this AuthorizationOptions opt)
        {
            opt.AddArticlePolitics();
        }

        private static void AddPoliticsHandlers(this IServiceCollection service)
        {
            service.AddArticleHandlers();
        }

        private static void AddServices(this IServiceCollection service)
        {
            service.AddSingleton<KafkaProducer>();
            service.AddScoped<IMessageRepository, MessageRepository>();
            service.AddAreaServices();
        }

        #endregion BuilderConfigure

        #region AppConfigure

        public static void Configure(this WebApplication app)
        {
            app.UseForwardedHeaders();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            using var scope = app.Services.CreateScope();
            MigrateDB(scope);
        }

        private static void MigrateDB(IServiceScope scope)
        {
            using var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
            db?.Database.Migrate();
        }

        #endregion AppConfigure
    }
}
