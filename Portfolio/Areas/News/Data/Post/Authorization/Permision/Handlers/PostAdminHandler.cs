using Microsoft.AspNetCore.Authorization;
using Portfolio.Areas.News.Models.Post;
using Portfolio.Models.Authorization;

namespace Portfolio.Areas.News.Data.Post.Authorization.Permision.Handlers
{
    public class PostAdminHandler : AuthorizationHandler<PostPermisionRequrement, PostModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PostPermisionRequrement requirement, PostModel resource)
        {
            var adminRoleName = Role.GetDefaultAdmin().Name;

            if (context.User.IsInRole(adminRoleName))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
