using Microsoft.AspNetCore.Authorization;
using Portfolio.Areas.News.Data.Post.Authorization.Permision;
using Portfolio.Areas.News.Data.Post.Authorization.Permision.Handlers;

namespace Portfolio.Areas.News.Data.Post.Authorization
{
    public static class HandlerRegistrar
    {
        public static void AddPostAuthorizationHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, PostAuthorHandler>();
            services.AddSingleton<IAuthorizationHandler, PostAdminHandler>();
        }

        public static void AddPostPolitics(this AuthorizationOptions options)
        {
            options.AddPolicy("PostPermision", policy => policy.Requirements.Add(new PostPermisionRequrement()));
        }
    }
}
