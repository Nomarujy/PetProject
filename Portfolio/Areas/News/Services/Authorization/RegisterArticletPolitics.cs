using Microsoft.AspNetCore.Authorization;
using Portfolio.Areas.News.Services.Authorization.Access.Read;
using Portfolio.Areas.News.Services.Authorization.Access.Update;

namespace Portfolio.Areas.News.Services.Authorization
{
    public static class RegisterArticletPolitics
    {
        public static void AddArticleHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, ArticleReadEveryoneHandler>();
            services.AddSingleton<IAuthorizationHandler, ArticleReadAuthorHandler>();
            services.AddSingleton<IAuthorizationHandler, ArticleReadAdminHandler>();

            services.AddSingleton<IAuthorizationHandler, ArticleUpdateAuthorHandler>();
            services.AddSingleton<IAuthorizationHandler, ArticleUpdateAdminHandler>();
        }

        public static void AddArticlePolitics(this AuthorizationOptions options)
        {
            options.AddPolicy("News_Read", opt => opt.Requirements.Add(new ArticleReadRequirement()));
            options.AddPolicy("News_Update", opt => opt.Requirements.Add(new PostUpdateRequirement()));
        }
    }
}
