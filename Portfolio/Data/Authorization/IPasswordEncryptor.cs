namespace Portfolio.Data.Authorization
{
    public interface IPasswordEncryptor
    {
        string EncryptPassword(string password);
        bool PasswordEqual(string password, string Hash);
    }
}