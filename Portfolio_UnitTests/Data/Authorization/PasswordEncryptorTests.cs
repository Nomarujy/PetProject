using Portfolio.Data.Authorization;

namespace Portfolio_UnitTests.Data.Authorization
{
    public class PasswordEncryptorTests
    {
        private readonly PasswordEncryptor passwordEncryptor;
        public PasswordEncryptorTests()
        {
            this.passwordEncryptor = new();
        }

        [Fact]
        public void ServiceHashingPassword()
        {
            string password = "SuperSecretPassword";

            string Hash = passwordEncryptor.EncryptPassword(password);

            Assert.NotEqual(password, Hash);
        }

        [Fact]
        public void OnePasswordHaveDiffirentHashes()
        {
            string password = "SuperSecretPassword";

            string Hash1 = passwordEncryptor.EncryptPassword(password);
            string Hash2 = passwordEncryptor.EncryptPassword(password);

            Assert.NotEqual(Hash1, Hash2);
        }

        [Fact]
        public void PasswordEqualReturnTrueOnSamePassword()
        {
            string password = "SuperSecretPassword";
            string Hash1 = passwordEncryptor.EncryptPassword(password);

            bool passwordEqual = passwordEncryptor.PasswordEqual(password, Hash1);

            Assert.True(passwordEqual);
        }

        [Fact]
        public void PasswordEqualReturnFalseOndifferentPassword()
        {
            string password = "SuperSecretPassword";
            string password2 = "NotCorrectPassword";
            string Hash1 = passwordEncryptor.EncryptPassword(password);

            bool passwordEqual = passwordEncryptor.PasswordEqual(password2, Hash1);

            Assert.False(passwordEqual);
        }
    }
}
