using Microsoft.AspNetCore.Mvc;
using Moq;
using Portfolio.Controls;
using Portfolio.Data.Database.ContactService;
using Portfolio.Models;
using Microsoft.Extensions.Logging;

namespace Portfolio_UnitTests.Controls
{
    public class HomeTest
    {
        private readonly Mock<IContactRepository> contactRepositoryMock;
        private readonly HomeController controller;
        private readonly Mock<ILogger<HomeController>> loggerMock;

        public HomeTest()
        {
            contactRepositoryMock = new();
            loggerMock = new();
            controller = new(contactRepositoryMock.Object, loggerMock.Object);
        }

        [Fact]
        public void IndexReturnPage()
        {
            var result = controller.Index();

            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexReturnErrorPage_If_ModelNotValid()
        {
            Contact contact = new();
            controller.ModelState.AddModelError("Any", "Any");

            var result = controller.Index(contact) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("ModelError", result!.ViewName);
        }

        [Fact]
        public void IndexWriteInDb()
        {
            Contact contact = new();

            var result = controller.Index(contact);

            contactRepositoryMock.Verify(r => r.Add(contact));
        }
    }
}
