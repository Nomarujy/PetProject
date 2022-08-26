using Microsoft.AspNetCore.Authorization;
using Portfolio.Areas.News.Models.Entity;

namespace Portfolio.Areas.News.Services.Authorization.Access.Read
{
    public class ArticleReadEveryoneHandler : AuthorizationHandler<ArticleReadRequirement, Article>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ArticleReadRequirement requirement, Article resource)
        {
            if (resource.IsDeleted == false && resource.IsPubleshed)
            {

            }
            return Task.CompletedTask;
        }
    }
}
