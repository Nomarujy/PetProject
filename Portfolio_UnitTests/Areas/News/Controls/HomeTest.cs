using Microsoft.AspNetCore.Authorization;
using Portfolio.Areas.News.Controls;
using Portfolio.Areas.News.Models.Entity;
using Portfolio.Areas.News.Models.ViewModel;
using Portfolio.Areas.News.Models.ViewModel.Home;
using Portfolio.Areas.News.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_UnitTests.Areas.News.Controls
{
    public class HomeTest
    {
        private readonly Mock<IArticleRepository> _databaseMock;
        private readonly Mock<IAuthorizationService> _authorizationServiceMock;

        private readonly HomeController controller;

        public HomeTest()
        {
            Article article = new()
            {
                Id = 2
            };

            _databaseMock = new();
            _databaseMock.Setup(c => c.GetRecentlyAsync(It.IsAny<int>())).ReturnsAsync(new RecentlyViewModel());
            _authorizationServiceMock = new();

            controller = new(_databaseMock.Object, _authorizationServiceMock.Object);
        }

        [Fact]
        public async Task IndexReturnViewWithModel()
        {
            var res = await controller.Index() as ViewResult;

            Assert.NotNull(res);
            Assert.True(res.Model is RecentlyViewModel);

            _databaseMock.Verify();
        }

        [Theory, MemberData(nameof(ReadData))]
        public async Task ReadReturnCorrectResults(int Id, Type expectedType)
        {
            var res = await controller.Read(Id);

            Assert.IsType(expectedType, res);
        }

        public static IEnumerable<object[]> ReadData()
        {
            yield return new object[] { 0, typeof(NotFoundResult) };
            //yield return new object[] { 1, typeof(ForbidResult) };
            //yield return new object[] { 2, typeof(ViewResult) };
        }
    }
}
