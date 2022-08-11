using Microsoft.AspNetCore.Authorization;
using Portfolio.Areas.News.Models.Entity;
using System.Security.Claims;

namespace Portfolio.Areas.News.Services.Authorization.Access.Read
{
    public class ArticleReadAuthorHandler : AuthorizationHandler<ArticleReadRequirement, Article>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ArticleReadRequirement requirement, Article resource)
        {
            string authorId = resource.AuthorId;
            string userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (authorId.Equals(userId))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
