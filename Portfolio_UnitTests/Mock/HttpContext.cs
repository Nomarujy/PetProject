using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace Portfolio_UnitTests.Mock
{
    public class MockHttpContext
    {
        public static Mock<HttpContext> Initial()
        {
            Mock<HttpContext> context = new();
            context.Setup(c => c.Connection.RemoteIpAddress).Returns(new System.Net.IPAddress(new byte[4]));

            return context;
        }

        public static HttpContext GetMockObject()
        {
            return Initial().Object;
        }
    }
}
