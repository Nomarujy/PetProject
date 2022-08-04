using Portfolio.Data.Account.Encryptor;

namespace Portfolio_UnitTests.Data.Account.Encryptor
{
    public class PasswordEncryptorTests
    {
        private readonly PasswordEncryptor encryptor;

        public PasswordEncryptorTests()
        {
            encryptor = new();
        }

        [Fact]
        public void EncryptPasswordGenerateHash()
        {
            string password = "SuperSecretPassword";

            string Hash = encryptor.EncryptPassword(password);

            Assert.NotEqual(password, Hash);
        }

        [Fact]
        public void EncryptPasswordGenerateDiferentHashes()
        {
            string password = "SuperSecretPassword";

            string Hash1 = encryptor.EncryptPassword(password);
            string Hash2 = encryptor.EncryptPassword(password);

            Assert.NotEqual(Hash1, Hash2);
        }

        [Fact]
        public void PasswordEqualReturnSamePassword()
        {
            string password = "SuperSecretPassword";
            string Hash1 = encryptor.EncryptPassword(password);

            bool passwordEqual = encryptor.PasswordEqual(password, Hash1);

            Assert.True(passwordEqual);
        }

        [Fact]
        public void PasswordEqualDifferentPassword()
        {
            string password = "SuperSecretPassword";
            string password2 = "NotCorrectPassword";
            string Hash1 = encryptor.EncryptPassword(password);

            bool passwordEqual = encryptor.PasswordEqual(password2, Hash1);

            Assert.False(passwordEqual);
        }
    }
}
