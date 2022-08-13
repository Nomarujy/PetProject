using System.Security.Claims;

namespace Portfolio.Areas.News.Services.Authorization.Access
{
    public static class NewsClaim
    {
        public static Claim Read { get => new Claim("News", "Read"); }
        public static Claim Update { get => new Claim("News", "Update"); }
    }
}
