using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Portfolio_UnitTests.Mock
{
    public static class HttpContextMock
    {
        public static HttpContext Get()
        {
            Mock<HttpContext> context = new();

            InitUser(context);

            return context.Object;
        }

        private static void InitUser(Mock<HttpContext> context)
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();

            context.Setup(c => c.User).Returns(claimsPrincipal);
        }

    }
}
