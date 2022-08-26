using Microsoft.AspNetCore.Authorization;

namespace Portfolio.Areas.News.Services.Authorization.Access.Update
{
    public class ArticleUpdateAdminHandler : AuthorizationHandler<ArticleUpdateRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ArticleUpdateRequirement requirement)
        {
            var updateClaim = NewsClaim.Update;

            if (context.User.HasClaim(c => c.Equals(updateClaim)))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
