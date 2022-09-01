using Portfolio.Areas.News.Controls;
using Portfolio.Areas.News.Models.ViewModel.Analytics;
using Portfolio.Areas.News.Models.ViewModel.Home;
using Portfolio.Areas.News.Services.Repository;
using Portfolio_UnitTests.Mock;

namespace Portfolio_UnitTests.Areas.News.Controls
{
    public class AnalyticsTest
    {
        private readonly Mock<IArticleRepository> _databaseMock;

        private readonly AnalyticsController controller;

        public AnalyticsTest()
        {
            _databaseMock = new();
            _databaseMock.Setup(c => c.GetArticleAnaliticsAsync(1)).ReturnsAsync(new AnalyticModel());



            controller = new(_databaseMock.Object);
            controller.ControllerContext.HttpContext = HttpContextMock.Get();
        }

        [Fact]
        public async Task MyArticleReturnView()
        {
            var res = await controller.MyArticles() as ViewResult;

            Assert.NotNull(res);
            Assert.True(res.Model is IEnumerable<DisplayArticleModel>);
            _databaseMock.Verify(c => c.GetAuthorArticlesAsync(It.IsAny<string>(), 0, 10));
        }

        [Fact]
        public async Task ArticleReturnView()
        {
            var res = await controller.Article(1) as ViewResult;

            Assert.NotNull(res);
            Assert.True(res.Model is AnalyticModel);
            _databaseMock.Verify();
        }
    }
}
