using Portfolio.Controls;
using Portfolio.Data.Account.Encryptor;
using Portfolio.Data.Account.Repository;
using Portfolio.Models.Authorization;
using Portfolio_UnitTests.Mock;

namespace Portfolio_UnitTests.Controls
{
    public class AccountTests
    {
        private readonly Mock<ILogger<AccountController>> logger;
        private readonly Mock<IAccountRepository> database;
        private readonly Mock<IPasswordEncryptor> encryptor;

        private readonly AccountController controller;

        public AccountTests()
        {
            logger = new();
            database = new();
            encryptor = new();

            database.Setup(d => d.GetUserWithRole(LogForm.Email)).Returns(new User() { Password = LogForm.Password });
            encryptor.Setup(e => e.PasswordEqual(LogForm.Password, LogForm.Password)).Returns(false);

            controller = new(database.Object, encryptor.Object, logger.Object);

            controller.ControllerContext.HttpContext = MockHttpContext.GetMockObject();
        }

        [Fact]
        public void AccessDeniedReturnpage()
        {
            var page = controller.AccessDenied();

            Assert.True(page is ViewResult);
        }

        [Fact]
        public void RegisterReturnpage()
        {
            var page = controller.Register();

            Assert.True(page is ViewResult);
        }

        [Fact]
        public void LoginReturnpage()
        {
            var page = controller.Login();

            Assert.True(page is ViewResult);
        }

        [Fact]
        public void LogoutReturnSignOutResult()
        {
            var page = controller.Logout();

            Assert.True(page is SignOutResult);
        }

        [Fact]
        public void RegisterReturnBadRequest()
        {
            controller.ModelState.AddModelError("Any", "any");
            var res = controller.Register(new RegisterForm());

            Assert.True(res is BadRequestObjectResult);
        }

        [Fact]
        public void RegisterAddToDatabase()
        {
            var res = controller.Register(new RegisterForm());

            Assert.True(res is RedirectResult);
        }

        [Fact]
        public void LoginNotFoundUser()
        {
            LoginForm form = new();

            var res = controller.Login(form, null);

            database.Verify(d => d.GetUserWithRole(form.Email));
            Assert.True(res is BadRequestObjectResult);
        }

        private readonly LoginForm LogForm = new() { Email = "TestMail", Password = "TestPas2" };

        [Fact]
        public void LoginPasswordNotEquals()
        {
            var res = controller.Login(LogForm, null);

            Assert.True(res is BadRequestResult);
        }
    }
}
