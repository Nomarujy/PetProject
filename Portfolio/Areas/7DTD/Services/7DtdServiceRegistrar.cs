
using Portfolio.Areas._7DTD.Services.Repository;
using Portfolio.Areas.News.Services.Repository;

namespace Portfolio.Areas._7DTD.Services
{
    public static class _7DtdServiceRegistrar
    {
        public static void Add7DtdServices(this IServiceCollection services)
        {
            services.AddSingleton<IBloodNightRepository, BloodNightRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
        }
    }
}
