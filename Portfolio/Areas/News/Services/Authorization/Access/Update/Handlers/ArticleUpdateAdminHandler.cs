using Microsoft.AspNetCore.Authorization;

namespace Portfolio.Areas.News.Services.Authorization.Access.Update
{
    public class ArticleUpdateAdminHandler : AuthorizationHandler<PostUpdateRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PostUpdateRequirement requirement)
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
