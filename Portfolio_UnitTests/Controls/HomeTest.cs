using Portfolio.Controls;
using Portfolio.Data.Contact.Repository;
using Portfolio.Models.Contact;
using Portfolio_UnitTests.Mock;

namespace Portfolio_UnitTests.Controls
{
    public class HomeTest
    {
        private readonly Mock<IContactRepository> databaseMock;
        private readonly Mock<ILogger<HomeController>> loggerMock;

        private readonly HomeController controller;


        public HomeTest()
        {
            databaseMock = new();
            loggerMock = new();

            controller = new(databaseMock.Object, loggerMock.Object);
            controller.ControllerContext.HttpContext = MockHttpContext.GetMockObject();
        }

        [Fact]
        public void IndexReturnPage()
        {
            var page = controller.Index();
            Assert.True(page is ViewResult);
        }

        [Fact]
        public void MessagesReturnPage()
        {
            var page = controller.Messages() as ViewResult;

            Assert.NotNull(page);
            Assert.True(page?.Model is ContactModel[]);
            databaseMock.Verify(c => c.GetFirst(0, 10));
        }

        [Fact]
        public void MessagesReturnModelByDescending()
        {
            controller.Messages(true, 0, 1);

            databaseMock.Verify(c => c.GetLast(0, 1));
        }

        [Fact]
        public void IndexReturnErrorPage_If_ModelNotValid()
        {
            ContactModel contact = new();
            controller.ModelState.AddModelError("Any", "Any");

            var result = controller.Index(contact) as ViewResult;

            Assert.Equal("ModelError", result?.ViewName);
        }

        [Fact]
        public void IndexWriteInDatabase()
        {
            ContactModel contact = new();

            var res = controller.Index(contact);

            Assert.True(res is RedirectResult);
            databaseMock.Verify(d => d.Add(contact));
        }

    }
}
