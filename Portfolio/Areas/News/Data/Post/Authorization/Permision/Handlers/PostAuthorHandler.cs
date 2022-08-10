using Microsoft.AspNetCore.Authorization;
using Portfolio.Areas.News.Models.Post;
using System.Security.Claims;

namespace Portfolio.Areas.News.Data.Post.Authorization.Permision.Handlers
{
    public class PostAuthorHandler : AuthorizationHandler<PostPermisionRequrement, PostModel>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PostPermisionRequrement requirement, PostModel resource)
        {
            string AuthorEmail = resource.AuthorEmail;
            string? UserEmail = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (AuthorEmail.Equals(UserEmail)) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
