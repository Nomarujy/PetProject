using Portfolio.Areas._7DTD.Controls;
using Portfolio.Areas._7DTD.Models;
using Portfolio.Areas._7DTD.Services.Repository;

namespace Portfolio_UnitTests.Areas._7DTD.Controls
{
    public class BloodNightTest
    {
        private readonly BloodNightController _controller;

        private readonly Mock<IBloodNightRepository> _repositoryMock;
        private readonly Mock<ILogger<BloodNightController>> _logerMock;

        public BloodNightTest()
        {
            _repositoryMock = new();
            _repositoryMock.Setup(c => c.InitServerTime(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()));
            _repositoryMock.Setup(c => c.GetView()).Returns(new BloodNightView(new ServerTime()));
            _logerMock = new();

            _controller = new(_repositoryMock.Object, _logerMock.Object);
        }

        [Fact]
        public void Index()
        {
            var result = _controller.Index() as ViewResult;

            Assert.NotNull(result);
            Assert.IsType<BloodNightView>(result.Model);
        }

        [Fact]
        public void UpdateNotValidModel()
        {
            InputForm form = new();
            _controller.ModelState.AddModelError("ER", "ER");

            var res = _controller.Update(form);

            Assert.True(res is BadRequestResult);
        }

        [Fact]
        public void UpdateSuccess()
        {
            InputForm form = new()
            {
                Day = 1,
                Hour = 1,
                MinsPerDay = 60
            };
            var res = _controller.Update(form);

            Assert.True(res is RedirectToActionResult);
            _repositoryMock.Verify();
        }
    }
}
