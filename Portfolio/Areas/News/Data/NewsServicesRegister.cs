using Portfolio.Areas.News.Data.Post;

namespace Portfolio.Areas.News.Data
{
    public static class NewsServicesRegister
    {
        public static void AddNewsServices(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
        }
    }
}
