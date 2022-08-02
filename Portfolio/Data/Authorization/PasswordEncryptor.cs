using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Portfolio.Data.Authorization
{
    public class PasswordEncryptor : IPasswordEncryptor
    {
        private const int SaltLenght = 16;
        private const int HashLenght = 64;
        private const int PasswordLenght = HashLenght - SaltLenght;

        private const int EncryptCount = 1000;
        private const int bytesRequested = HashLenght - SaltLenght;

        public string EncryptPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltLenght);

            byte[] result = Encrypt(password, salt);

            return Convert.ToBase64String(result);
        }

        public bool PasswordEqual(string password, string Hash)
        {
            byte[] salt = GetSaltFromHash(Hash);

            string HashedPassword = Convert.ToBase64String(Encrypt(password, salt));
            return Hash.Equals(HashedPassword);
        }

        private static byte[] Encrypt(string password, byte[] salt)
        {
            var hashed = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, EncryptCount, bytesRequested)
                .Take(PasswordLenght).ToArray();

            byte[] result = new byte[HashLenght];

            salt.CopyTo(result, 0);
            hashed.CopyTo(result, SaltLenght);

            return result;
        }

        private static byte[] GetSaltFromHash(string Hash)
        {
            byte[] salt = Convert.FromBase64String(Hash);
            return salt.Take(SaltLenght).ToArray();
        }
    }
}
