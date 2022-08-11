using Microsoft.AspNetCore.Authorization;
using Portfolio.Areas.News.Models.Entity;

namespace Portfolio.Areas.News.Services.Authorization.Access.Read
{
    public class ArticleReadAdminHandler : AuthorizationHandler<ArticleReadRequirement, Article>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ArticleReadRequirement requirement, Article resource)
        {
            var readClaim = NewsClaim.Read;

            if (context.User.HasClaim(c=> c.Equals(readClaim)))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
