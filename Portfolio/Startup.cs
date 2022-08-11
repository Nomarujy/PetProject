using Microsoft.EntityFrameworkCore;
using Portfolio.Areas._7DTD.Data.BloodNightRepository;
using Portfolio.Models.Authentication.Entity;
using Portfolio.Models;
using Portfolio.Areas.News.Services.Authorization;
using Portfolio.Areas.News.Services.Repository;

namespace Portfolio
{
    public static class Startup
    {
        public static void AddDbAndAuthServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(connectionString));

            services.AddAuthentication("Cookies").AddCookie();
            services.AddAuthorization(opt=>
            {
                opt.AddArticlePolitics();
            });
            services.AddIdentity<User, Role>(opt => opt.Password.RequireNonAlphanumeric = false).AddEntityFrameworkStores<ApplicationDbContext>();

            //Handlers
            services.AddArticleHandlers();

        }

        public static void AddLocalServices(this IServiceCollection service)
        {
        }

        public static void AddAreaServices(this IServiceCollection services)
        {
            services.AddSingleton<IBloodNightRepository, BloodNightRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
        }

    }
}
